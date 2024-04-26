namespace Domain.Core.Models
{
    public class Pacient
    {
        private Pacient(Guid primarykey,
            string login, string fio, string password, string? analysis = null, string? conclusion = null,
            IEnumerable<Allergen>? allergens = null, IEnumerable<Product>? products = null, IEnumerable<Recipe>? recipes = null)
        {
            Primarykey = primarykey;
            Login = login;
            Fio = fio;
            Password = password;
            Analysis = analysis;
            Conclusion = conclusion;
            Allergens = allergens;
            FavoriteProducts = products;
            FavoriteRecipes = recipes;
        }

        public Guid Primarykey { get; }

        public string Login { get; }
        public string Password { get; }
        public string Fio { get; }


        public string? Analysis { get; } = string.Empty;
        public string? Conclusion { get; } = string.Empty;

        public IEnumerable<Allergen>? Allergens { get; set; }
        public IEnumerable<Product>? FavoriteProducts { get; }
        public IEnumerable<Recipe>? FavoriteRecipes { get; }

        public static Pacient CreatePacient (Guid primarykey,
            string login, string fio, string password, string? analysis = null, string? conclusion = null,
            IEnumerable<Allergen>? allergens = null, IEnumerable<Product>? products = null, IEnumerable<Recipe>? recipes = null)
        {

            var pacient = new Pacient(primarykey, login, fio, password, analysis, conclusion, allergens, products, recipes);

            return pacient;
        }
    }
}
