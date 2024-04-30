using Domain.Core.Models;

namespace Domain.Services.Interfaces
{
    public interface ITherapeuticNutritionService
    {
        Task<Pacient?> GetPacientByLogin(string login);
        Task<Pacient?> ChangeFavorite(string login, string type, Guid primarykey, bool isFavorite);

        Task<List<Allergen>?> GetAllergens(string? login);
        Task<List<Product>?> GetProducts(string? login);
        Task<List<Recipe>?> GetRecipes(string? login);
    }
}