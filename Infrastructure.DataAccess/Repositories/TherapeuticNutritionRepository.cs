using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Models;
using Infrastructure.DataAccess.Entities.Relations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories
{
    public class TherapeuticNutritionRepository : ITherapeuticNutritionRepository
    {
        private readonly TherapeuticNutritionDbContext _context;
        private readonly IMapper _mapper;

        public TherapeuticNutritionRepository(TherapeuticNutritionDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region CRUD
        public async Task Add(Pacient pacient)
        {
            var pacientEntity = new Entities.Pacient()
            {
                Primarykey = pacient.Primarykey,
                Login = pacient.Login,
                Fio = pacient.Fio,
                Password = pacient.Password,
                Analysis = pacient.Analysis,
                Сonclusion = pacient.Conclusion
            };
            await _context.Pacients.AddAsync(pacientEntity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Pacient
        public async Task<Pacient?> GetPacientByLogin(string login)
        {
            var pacientEnity = await _context.Pacients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Login == login);
            if (pacientEnity == null) { return null; }

            var pacientModel = Pacient.CreatePacient(pacientEnity.Primarykey, pacientEnity.Login,
                pacientEnity.Fio, pacientEnity.Password, pacientEnity.Analysis, pacientEnity.Сonclusion);
            return pacientModel;
        }
        #endregion

        #region Allergen
        public async Task<List<Allergen>> GetAllergens()
        {
            var allergenEntities = await _context.Allergens
                .AsNoTracking()
                .ToListAsync();
            var allergens = allergenEntities
                .Select(e => Allergen.CreateAllergen(e.Primarykey, e.Name, e.Reaction, e.DangerDegree))
                .ToList();

            return allergens;
        }

        public async Task<List<Allergen>> GetPacientAllergens(Guid pacientKey)
        {
            var pacientAllergens = await _context.PacientAllergens
                .Where(e => e.Pacient == pacientKey)
                .AsNoTracking()
                .ToListAsync();

            var pacientAllergenKeys = pacientAllergens
                .Select(e => e.Allergen);
            var allergenEntities = await _context.Allergens
                .Where(e => pacientAllergenKeys.Contains(e.Primarykey))
                .AsNoTracking()
                .ToListAsync();

            var allergens = allergenEntities
                .Select(e => Allergen.CreateAllergen(e.Primarykey, e.Name, e.Reaction, e.DangerDegree, true))
                .ToList();

            return allergens;
        }

        public async Task<Allergen?> GetAllergenByPrimarykey(Guid primarykey)
        {
            var allergenEnity = await _context.Allergens
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Primarykey == primarykey);
            if (allergenEnity == null) { return null; }

            var allergenModel = Allergen.CreateAllergen(allergenEnity.Primarykey, allergenEnity.Name,
                allergenEnity.Reaction, allergenEnity.DangerDegree);
            return allergenModel;
        }
        #endregion

        #region Product
        public async Task<List<Product>> GetProducts()
        {
            var productEntities = await _context.Products
                .AsNoTracking()
                .ToListAsync();
            var products = productEntities
                .Select(e => Product.CreateProduct(e.Primarykey, e.Name, e.NutritionalValue))
                .ToList();

            return products;
        }

        public async Task<List<Product>> GetFavoriteProducts(Guid pacientKey)
        {
            var favoriteProducts = await _context.FavoriteProducts
                .Where(e => e.Pacient == pacientKey)
                .AsNoTracking()
                .ToListAsync();

            var favoriteProductKeys = favoriteProducts
                .Select(e => e.Product);
            var productEntities = await _context.Products
                .Where(e => favoriteProductKeys.Contains(e.Primarykey))
                .AsNoTracking()
                .ToListAsync();

            var products = productEntities
                .Select(e => Product.CreateProduct(e.Primarykey, e.Name, e.NutritionalValue, true))
                .ToList();

            return products;
        }

        public async Task<Product?> GetProductByPrimarykey(Guid primarykey)
        {
            var productEnity = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Primarykey == primarykey);
            if (productEnity == null) { return null; }

            var productModel = Product.CreateProduct(productEnity.Primarykey, productEnity.Name, productEnity.NutritionalValue);
            return productModel;
        }
        #endregion

        #region Recipe
        public async Task<List<Recipe>> GetRecipes()
        {
            var recipeEntities = await _context.Recipes
                .AsNoTracking()
                .ToListAsync();
            var recipes = recipeEntities
                .Select(e => Recipe.CreateRecipe(e.Primarykey, e.Name, e.Сalories, e.Rating))
                .ToList();
            return recipes;
        }

        public async Task<List<Recipe>> GetFavoriteRecipes(Guid pacientKey)
        {
            var favoriteRecipes = await _context.FavoriteRecipes
                .Where(e => e.Pacient == pacientKey)
                .AsNoTracking()
                .ToListAsync();

            var favoriteRecipeKeys = favoriteRecipes
                .Select(e => e.Recipe);
            var recipeEntities = await _context.Recipes
                .Where(e => favoriteRecipeKeys.Contains(e.Primarykey))
                .AsNoTracking()
                .ToListAsync();

            var recipes = recipeEntities
                .Select(e => Recipe.CreateRecipe(e.Primarykey, e.Name, e.Сalories, e.Rating, true))
                .ToList();

            return recipes;
        }

        public async Task<Recipe?> GetRecipeByPrimarykey(Guid primarykey)
        {
            var recipeEnity = await _context.Recipes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Primarykey == primarykey);
            if (recipeEnity == null) { return null; }

            var recipeModel = Recipe.CreateRecipe(recipeEnity.Primarykey, recipeEnity.Name,
                recipeEnity.Сalories, recipeEnity.Rating);
            return recipeModel;
        }
        #endregion

        #region Relations
        public async Task AddPacientAllergen(Pacient pacient, Allergen allergen)
        {
            var pacientEntity = _context.Pacients
                .FirstOrDefault(e => e.Primarykey == pacient.Primarykey);
            if (pacientEntity == null)
                return;

            var pacientAllergenEntity = new PacientAllergen()
            {
                Primarykey = Guid.NewGuid(),
                Pacient = pacient.Primarykey,
                Allergen = allergen.Primarykey
            };
            await _context.PacientAllergens.AddAsync(pacientAllergenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePacientAllergen(Pacient pacient, Allergen allergen)
        {
            var pacientEntity = _context.Pacients
                .FirstOrDefault(e => e.Primarykey == pacient.Primarykey);
            if (pacientEntity == null)
                return;

            var pacientAllergenEntity = await _context.PacientAllergens
                .FirstOrDefaultAsync(e => e.Pacient == pacient.Primarykey && e.Allergen == allergen.Primarykey);
            if (pacientAllergenEntity == null)
                return;

            _context.PacientAllergens.Remove(pacientAllergenEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddFavoriteProduct(Pacient pacient, Product product)
        {
            var pacientEntity = _context.Pacients
                .FirstOrDefault(e => e.Primarykey == pacient.Primarykey);
            if (pacientEntity == null)
                return;

            var favoriteProductEntity = new FavoriteProduct()
            {
                Primarykey = Guid.NewGuid(),
                Pacient = pacient.Primarykey,
                Product = product.Primarykey
            };
            await _context.FavoriteProducts.AddAsync(favoriteProductEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFavoriteProduct(Pacient pacient, Product product)
        {
            var pacientEntity = _context.Pacients
                .FirstOrDefault(e => e.Primarykey == pacient.Primarykey);
            if (pacientEntity == null)
                return;

            var favoriteProductEntity = await _context.FavoriteProducts
                .FirstOrDefaultAsync(e => e.Pacient == pacient.Primarykey && e.Product == product.Primarykey);
            if (favoriteProductEntity == null)
                return;

            _context.FavoriteProducts.Remove(favoriteProductEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddFavoriteRecipe(Pacient pacient, Recipe recipe)
        {
            var recipeEntity = _context.Recipes
                .FirstOrDefault(e => e.Primarykey == recipe.Primarykey);
            if (recipeEntity == null)
                return;

            var favoriteRecipeEntity = new FavoriteRecipe()
            {
                Primarykey = Guid.NewGuid(),
                Pacient = pacient.Primarykey,
                Recipe = recipe.Primarykey
            };
            await _context.FavoriteRecipes.AddAsync(favoriteRecipeEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFavoriteRecipe(Pacient pacient, Recipe recipe)
        {
            var recipeEntity = _context.Recipes
                .FirstOrDefault(e => e.Primarykey == recipe.Primarykey);
            if (recipeEntity == null)
                return;

            var favoriteRecipeEntity = await _context.FavoriteRecipes
                .FirstOrDefaultAsync(e => e.Pacient == pacient.Primarykey && e.Recipe == recipe.Primarykey);
            if (favoriteRecipeEntity == null)
                return;

            _context.FavoriteRecipes.Remove(favoriteRecipeEntity);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
