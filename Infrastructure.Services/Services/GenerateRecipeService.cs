using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Domain.Core.Models;
using Domain.Services;
using Domain.Services.Interfaces;
using GigaChatAdapter;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.DataAccess.Repositories;
using GigaChatAdapter.Completions;
using System.Text.Json;
using System.Threading;
using LikhodedDynamics.Sber.GigaChatSDK;
using LikhodedDynamics.Sber.GigaChatSDK.Models;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Services.Services
{
    public class GenerateRecipeService : IGenerateRecipeService
    {
        //private readonly Authorization _adapterAuth;
        //private readonly Completion _completion;
        //private readonly CompletionSettings _settings;
        private readonly GigaChat _gigaChat;
        private readonly ITherapeuticNutritionRepository _therapeuticNutritionRepository;
        private readonly IFileRepository _fileRepository;
        public GenerateRecipeService(IOptions<GigaChatOptions> options,
            ITherapeuticNutritionRepository therapeuticNutritionRepository,
            IFileRepository fileRepository) 
        {
            _therapeuticNutritionRepository = therapeuticNutritionRepository;
            _fileRepository = fileRepository;

            _gigaChat = new GigaChat(options.Value.AuthData, options.Value.IsCommercial,
                options.Value.IgnoreTLS, options.Value.SaveImage);
        }
        public async Task<Recipe?> GenerateRecipe(List<Product>? products)
        {
            if (products.IsNullOrEmpty() || products == null)
                return null;

            await _gigaChat.CreateTokenAsync();

            var prompt = CreatePrompt(products);
            Recipe? recipe = null;
            while (recipe == null)
            {
                var recipeResult = _gigaChat.CompletionsAsync(prompt).Result;
                var messageTextResponse = recipeResult?.choices?.LastOrDefault()?.message?.content;
                if (messageTextResponse == null || messageTextResponse.IsNullOrEmpty())
                    continue;
                recipe = await ParseResponce(messageTextResponse);
            }

            prompt = CreateImgPrompt(recipe.Name);
            while (true)
            {
                var imgResult = CompletionsImgAsync(prompt, _gigaChat.Token).Result;
                var imgResponse = imgResult?.choices?.LastOrDefault()?.message?.content;
                if (imgResponse == null || !imgResponse.Contains("src"))
                    return recipe;

                var fileId = _gigaChat.GetFileId(imgResponse);
                if (fileId != null)
                {
                    var _ = await _gigaChat.GetImageAsByteAsync(fileId);
                    await ParseImgResponce(recipe, fileId);
                    var temp = Directory.GetCurrentDirectory() + fileId;
                    break;
                }
            }

            return recipe;
        }

        private string CreatePrompt(List<Product> products)
        {
            var prompt = new StringBuilder();
            prompt.AppendLine("Предложи рецепт блюда из следующего набора продуктов:");
            prompt.AppendLine($"{string.Join(", ", products.Select(e => e.Name))}.");
            //prompt.AppendLine("Название рецепта напиши в кавычках.");
            //prompt.AppendLine("Далее приведи пошаговую инструкцию приготовления данного блюда.");
            prompt.AppendLine("Предоставь ответ в формате:");
            prompt.AppendLine("Название рецепта:.../" +
                "Иснтрукция по приготоволению данного блюда:.../" +
                "Калорийность данного блюда на 100 грамм (в виде вещественного числа):...");

            return prompt.ToString();
        }

        private async Task<Recipe?> ParseResponce(string responce)
        {
            var responceStr = responce.Split("\n\n");

            var nameIndex = Array.IndexOf(responceStr, responceStr.FirstOrDefault(e => e.Contains("Название")));
            string name;
            try
            {
                name = responceStr[nameIndex].Split(":")[1];
                if (name.Contains('\n'))
                    name = name.Split("\n")[0].Trim();
            }
            catch
            {
                return null;
            }
            if (name.IsNullOrEmpty())
                return null;

            var t = responceStr.FirstOrDefault(e => e.Contains("Инструкция"));
            var descriptionIndex = Array.IndexOf(responceStr, responceStr.FirstOrDefault(e => e.Contains("Инструкция"))) + 1;
            string description;
            try
            {
                description = responceStr[descriptionIndex];
            }
            catch
            {
                return null;
            }
            if (description.IsNullOrEmpty())
                return null;

            var caloriesIndex = Array.IndexOf(responceStr, responceStr.FirstOrDefault(e => e.Contains("Калорийность")));
            decimal calories;
            try
            {
                calories = Decimal.Parse(responceStr[caloriesIndex].Split(":").Last().Split(' ').First().Trim());
            }
            catch
            {
                calories = 0;
            }

            var recipe = Recipe.CreateRecipe(Guid.NewGuid(), name, description, calories, 0, false);
            await _therapeuticNutritionRepository.Add(recipe);

            return recipe;
        }

        private string CreateImgPrompt(string name)
        {
            var prompt = new StringBuilder();
            prompt.AppendLine("Ты профессиональный художник. " +
                "Если тебя просят создать изображение, ты должен сгенерировать специальный блок:" +
                " <fuse>text2image(query: str, style: str)</fuse>,\n" +
                $"где query — {name}" +
                " style — абстракция.");
            //prompt.AppendLine($"Найди изображение блюда (или похожего на него блюда): {name} и предоставь в качестве ответа адрес изображения (URL)");

            return prompt.ToString();
        }

        private async Task ParseImgResponce(Recipe recipe, string? responce)
        {
            var imgUrl = $"https://localhost:7253/TherapeuticNutrition/images/{responce}";
;
            await _fileRepository.Add(recipe.Primarykey, imgUrl);
        }

        private async Task<Response?> CompletionsImgAsync(string prompt, Token token)
        {
            var query = new MessageOptions()
            {
                messages = new List<MessageContent>() { { new MessageContent("user", prompt) } },
                model = "GigaChat-Pro",
                temperature = 1,
                top_p = (float)0.1,
                n = 1,
                stream = false,
                max_tokens = 512,
                function_call = "auto"
            };

            var httpClientHandler = new HttpClientHandler();

            using (HttpClient client = new HttpClient(httpClientHandler))
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, 
                    "https://gigachat.devices.sberbank.ru/api/v1/" + "chat/completions");

                httpRequestMessage.Headers.Add("Authorization", "Bearer " + token.AccessToken);
                httpRequestMessage.Content = new StringContent(JsonSerializer.Serialize(query));
                httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = await client.SendAsync(httpRequestMessage);
                request.EnsureSuccessStatusCode();

                var responce = await request.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Response>(responce);

                Console.WriteLine(responce);

                client.Dispose();

                return result;
            }
        }
    }
}
