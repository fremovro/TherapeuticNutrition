using Infrastructure.DataAccess.Entities;
using Infrastructure.DataAccess.Entities.Relations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class TherapeuticNutritionDbContext : DbContext
    {
        public TherapeuticNutritionDbContext(DbContextOptions<TherapeuticNutritionDbContext> options)
            : base (options)
        {
        }

        public DbSet<Pacient> Pacients { get; set; }

        public DbSet<Allergen> Allergens { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        #region Relations
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<PacientAllergen> PacientAllergens { get; set; }
        public DbSet<ProductAllergen> ProductAllergens { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        #endregion
    }
}
