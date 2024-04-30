using Domain.Core.Models;

namespace Domain.Core.Interfaces
{
    public interface ITherapeuticNutritionRepository
    {
        Task Add(Pacient pacient);

        Task<Pacient?> GetPacientByLogin(string login);

        Task<List<Allergen>> GetAllergens();
        Task<Allergen?> GetAllergenByPrimarykey(Guid primarykey);
        Task<List<Allergen>> GetPacientAllergens(Guid pacientKey);

        Task<List<Product>> GetProducts();
        Task<Product?> GetProductByPrimarykey(Guid primarykey);
        Task<List<Product>> GetFavoriteProducts(Guid pacientKey);

        Task<List<Recipe>> GetRecipes();
        Task<Recipe?> GetRecipeByPrimarykey(Guid primarykey);
        Task<List<Recipe>> GetFavoriteRecipes(Guid pacientKey);

        Task AddPacientAllergen(Pacient pacient, Allergen allergen);
        Task DeletePacientAllergen(Pacient pacient, Allergen allergen);
        Task AddFavoriteProduct(Pacient pacient, Product allergen);
        Task DeleteFavoriteProduct(Pacient pacient, Product allergen);
        Task AddFavoriteRecipe(Pacient pacient, Recipe recipe);
        Task DeleteFavoriteRecipe(Pacient pacient, Recipe recipe);
    }
}