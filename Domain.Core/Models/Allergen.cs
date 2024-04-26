namespace Domain.Core.Models
{
    public class Allergen
    {
        private Allergen(Guid primarykey, string name, string reaction, int dangerDegree, bool isFavorite) 
        { 
            Primarykey = primarykey;
            Name = name;
            Reaction = reaction;
            DangerDegree = dangerDegree;
            IsFavorite = isFavorite;
        }

        public Guid Primarykey { get; }

        public string Name { get; }
        public string Reaction { get; }
        public int DangerDegree { get; }
        public bool IsFavorite { get; set; }

        public static Allergen CreateAllergen(Guid primarykey, string name, string reaction, int dangerDegree, bool isFavorite = false)
        {
            var error = string.Empty;

            var pacient = new Allergen(primarykey, name, reaction, dangerDegree, isFavorite);

            return pacient;
        }
    }
}
