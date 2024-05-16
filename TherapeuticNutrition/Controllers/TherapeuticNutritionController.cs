using System.Drawing;
using System.IO;
using Domain.Core.Models;
using Domain.Services;
using Domain.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace TherapeuticNutrition.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TherapeuticNutritionController : ControllerBase
    {
        private readonly ITherapeuticNutritionService _therapeuticNutritionService;
        private readonly IContentProvider _fileProvider;
        private readonly IGenerateRecipeService _generateRecipeService;
        private readonly Domain.Services.Interfaces.IAuthorizationService _authorizationService;

        public TherapeuticNutritionController(ITherapeuticNutritionService therapeuticNutritionService, 
            Domain.Services.Interfaces.IAuthorizationService authorizationService,
            IGenerateRecipeService generateRecipeService,
            IContentProvider fileProvider)
        {
            _therapeuticNutritionService = therapeuticNutritionService;
            _authorizationService = authorizationService;
            _generateRecipeService = generateRecipeService;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        [Route("get/pacient")]
        public async Task<ActionResult<Pacient>> GetPacient()
        {
            if (Request.Cookies["token"] == null 
                || Request.Cookies["login"] == null)
            {
                return Unauthorized();
            }

            var login = Request.Cookies["login"]?.ToString();
            var pacient = await _therapeuticNutritionService.GetPacientByLogin(login);
            return Ok(pacient);
        }

        [HttpPost]
        [Route("generate/recipe/products={productKeys}")]
        public async Task<ActionResult<Pacient>> GenerateRecipe(string productKeys)
        {
            if (Request.Cookies["token"] == null
                || Request.Cookies["login"] == null)
            {
                return Unauthorized();
            }

            var login = Request.Cookies["login"]?.ToString();
            var products = _therapeuticNutritionService.GetProducts(login).Result?
                .Where(e => productKeys.Split("&").Select(p => Guid.Parse(p)).Contains(e.Primarykey)).ToList();
            var recipe = await _generateRecipeService.GenerateRecipe(products);

            return Ok(recipe);
        }

        [HttpPost]
        [Route("pacient/change/type={type}&primarykey={primarykey}&isFavorite={isFavorite}")]
        public async Task<IResult> ChangeFavorite(string type, Guid primarykey, bool isFavorite)
        {
            var login = Request.Cookies["login"]?.ToString();
            if (login == null)
            {
                return Results.Unauthorized();
            }
            var pacient = await _therapeuticNutritionService.ChangeFavorite(login, type, primarykey, isFavorite);
            return Results.Ok(pacient);
        }

        #region Прочие роуты
        [HttpGet]
        [Route("get/allergens")]
        public async Task<ActionResult<Pacient>> GetAllergens()
        {
            if (Request.Cookies["token"] == null
                || Request.Cookies["login"] == null)
            {
                return Unauthorized("Необходимо авторизоваться");
            }

            var login = Request.Cookies["login"]?.ToString();
            var allergens = await _therapeuticNutritionService.GetAllergens(login);
            return Ok(allergens);
        }

        [HttpGet]
        [Route("get/products")]
        public async Task<ActionResult<Pacient>> GetProducts()
        {
            if (Request.Cookies["token"] == null
                || Request.Cookies["login"] == null)
            {
                return Unauthorized("Необходимо авторизоваться");
            }

            var login = Request.Cookies["login"]?.ToString();
            var products = await _therapeuticNutritionService.GetProducts(login);
            return Ok(products);
        }

        [HttpGet]
        [Route("get/recipes")]
        public async Task<ActionResult<Recipe>> GetRecipes()
        {
            if (Request.Cookies["token"] == null
                || Request.Cookies["login"] == null)
            {
                return Unauthorized("Необходимо авторизоваться");
            }

            var login = Request.Cookies["login"]?.ToString();
            var recipes = await _therapeuticNutritionService.GetRecipes(login);
            return Ok(recipes);
        }
        #endregion

        #region Авторизация
        [HttpGet]
        [Route("login/login={login}&password={password}")]
        public async Task<IResult> Login(string login, string password)
        {
            try
            {
                var token = await _authorizationService.Login(login, password);

                var cookieOptions = new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                };
                Response.Cookies.Append("token", token, cookieOptions);
                Response.Cookies.Append("login", login, cookieOptions);

                var pacient = await _therapeuticNutritionService.GetPacientByLogin(login);
                return Results.Ok(pacient);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }

        [HttpPost("{login, password, fio}", Name = "register")]
        public async Task<IResult> Register(string login, string password, string fio)
        {
            await _authorizationService.Register(login, fio, password);

            return Results.Ok();
        }
        #endregion

        #region Файлы
        [HttpGet]
        [Route("get/image/relation={relation}")]
        public async Task<ActionResult<string>> GetImageUrl(Guid relation)
        {

            var img = await _fileProvider.GetImageUrl(relation);

            return Ok(img);
        }

        [HttpGet]
        [Route("images/{id}")]
        public FileResult Image(string id)
        {
            var path = Path.Combine([Directory.GetCurrentDirectory(), id + ".jpg"]);
            var img = new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
            return img;
        }
        #endregion
    }
}
