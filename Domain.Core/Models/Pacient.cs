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
            Allergens = allergens;
            FavoriteProducts = products;
            FavoriteRecipes = recipes;
        }

        public Guid Primarykey { get; }

        public string Login { get; }
        public string Password { get; }
        public string Fio { get; }


        public string? Analysis { get; } = string.Empty;
        public string? Сonclusion { get; } = string.Empty;

        public IEnumerable<Allergen>? Allergens { get; }
        public IEnumerable<Product>? FavoriteProducts { get; }
        public IEnumerable<Recipe>? FavoriteRecipes { get; }

        public static Pacient Create (Guid primarykey,
            string login, string fio, string password, string? analysis = null, string? conclusion = null,
            IEnumerable<Allergen>? allergens = null, IEnumerable<Product>? products = null, IEnumerable<Recipe>? recipes = null)
        {
            var error = string.Empty;

            //if (string.IsNullOrEmpty(login) || login.Length < 4 || login.Length > 12)
            //{
            //    error = "$Необходимо ввести логин";
            //}
            //if (login.Length < 4)
            //{
            //    error = "$Логин должен содержать более 4 символов";
            //}
            //if (login.Length > 12)
            //{
            //    error = "$Логин должен содержать не более 12 символов";
            //}
            //if (Regex.IsMatch(login, "^[a-zA-Z0-9]*$"))
            //{
            //    error = "$Логин должен состоять из латинских букв и цифр";
            //}

            var pacient = new Pacient(primarykey, login, fio, password, analysis, conclusion, allergens, products, recipes);

            return pacient;
        }
    }
}
