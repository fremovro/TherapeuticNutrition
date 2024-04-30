using Domain.Core.Interfaces;
using Domain.Core.Models;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class TherapeuticNutritionService : ITherapeuticNutritionService
    {
        private readonly ITherapeuticNutritionRepository _therapeuticNutritionRepository;

        public TherapeuticNutritionService(ITherapeuticNutritionRepository therapeuticNutritionRepository)
        {
            _therapeuticNutritionRepository = therapeuticNutritionRepository;
        }

        #region Pacient
        public async Task<Pacient?> GetPacientByLogin(string login)
        {
            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);
            if (pacient == null)
                return null;

            var allergens = await _therapeuticNutritionRepository.GetPacientAllergens(pacient.Primarykey);
            pacient.Allergens = allergens;

            var products = await _therapeuticNutritionRepository.GetFavoriteProducts(pacient.Primarykey);
            pacient.FavoriteProducts = products;

            var recipes = await _therapeuticNutritionRepository.GetFavoriteRecipes(pacient.Primarykey);
            pacient.FavoriteRecipes = recipes;

            return pacient;
        }

        public async Task<Pacient?> ChangeFavorite(string login, string type, Guid primarykey, bool isFavorite)
        {
            if (login == null)
                return null;

            var pacient = await GetPacientByLogin(login);
            if (pacient == null)
                return null;

            switch (type)
            {
                case nameof(Allergen):
                    var allergen = await _therapeuticNutritionRepository.GetAllergenByPrimarykey(primarykey);
                    if (allergen == null)
                        return pacient;

                    if (isFavorite)
                        await _therapeuticNutritionRepository.AddPacientAllergen(pacient, allergen);
                    else
                        await _therapeuticNutritionRepository.DeletePacientAllergen(pacient, allergen);
                    break;

                case nameof(Product):
                    var product = await _therapeuticNutritionRepository.GetProductByPrimarykey(primarykey);
                    if (product == null)
                        return pacient;

                    if (isFavorite)
                        await _therapeuticNutritionRepository.AddFavoriteProduct(pacient, product);
                    else
                        await _therapeuticNutritionRepository.DeleteFavoriteProduct(pacient, product);
                    break;

                case nameof(Recipe):
                    var recipe = await _therapeuticNutritionRepository.GetRecipeByPrimarykey(primarykey);
                    if (recipe == null)
                        return pacient;

                    if (isFavorite)
                        await _therapeuticNutritionRepository.AddFavoriteRecipe(pacient, recipe);
                    else
                        await _therapeuticNutritionRepository.DeleteFavoriteRecipe(pacient, recipe);
                    break;
            }

            return await GetPacientByLogin(login);
        }
        #endregion

        #region Other
        public async Task<List<Allergen>?> GetAllergens(string? login)
        {
            var allergens = await _therapeuticNutritionRepository.GetAllergens();
            if (login == null)
                return allergens;

            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);
            if (pacient == null)
                return allergens;

            var pacientAllergens = await _therapeuticNutritionRepository.GetPacientAllergens(pacient.Primarykey);

            foreach (var pacientAllergen in pacientAllergens)
            {
                var allergen = allergens?.FirstOrDefault(e => e.Primarykey == pacientAllergen.Primarykey);
                if (allergen == null) continue;

                allergen.IsFavorite = pacientAllergen.IsFavorite;
            }

            return allergens;
        }

        public async Task<List<Product>?> GetProducts(string? login)
        {
            var products = await _therapeuticNutritionRepository.GetProducts();
            if (login == null)
                return products;

            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);
            if (pacient == null)
                return products;

            var favoriteProducts = await _therapeuticNutritionRepository.GetFavoriteProducts(pacient.Primarykey);

            foreach (var favoriteProduct in favoriteProducts)
            {
                var product = products?.FirstOrDefault(e => e.Primarykey == favoriteProduct.Primarykey);
                if (product == null) continue;

                product.IsFavorite = favoriteProduct.IsFavorite;
            }

            return products;
        }

        public async Task<List<Recipe>?> GetRecipes(string? login)
        {
            var recipes = await _therapeuticNutritionRepository.GetRecipes();
            if (login == null)
                return recipes;

            var pacient = await _therapeuticNutritionRepository.GetPacientByLogin(login);
            if (pacient == null)
                return recipes;

            var favoriteRecipes = await _therapeuticNutritionRepository.GetFavoriteRecipes(pacient.Primarykey);

            foreach (var favoriteRecipe in favoriteRecipes)
            {
                var recipe = recipes?.FirstOrDefault(e => e.Primarykey == favoriteRecipe.Primarykey);
                if (recipe == null) continue;

                recipe.IsFavorite = favoriteRecipe.IsFavorite;
            }

            return recipes;
        }
        #endregion
    }
}
