using Domain.Core.Models;

namespace Infrastructure.Services.Interfaces
{
    public interface IGenerateRecipeService
    {
        Task<Recipe?> GenerateRecipe(List<Product>? products);
    }
}