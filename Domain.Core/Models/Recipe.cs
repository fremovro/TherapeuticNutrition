using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models
{
    public class Recipe
    {
        private Recipe(Guid primarykey, string name, string description, decimal сalories, decimal rating, bool isFavorite)
        {
            Primarykey = primarykey;
            Name = name;
            Description = description;
            Сalories = сalories;
            Rating = rating;
            IsFavorite = isFavorite;
        }

        public Guid Primarykey { get; }

        public string Name { get; }
        public string Description { get; }
        public decimal Сalories { get; }
        public decimal Rating { get; }
        public bool IsFavorite { get; set; }

        public static Recipe CreateRecipe(Guid primarykey, string name, string description, decimal сalories,
            decimal rating, bool isFavorite = false)
        {
            var recipe = new Recipe(primarykey, name, description, сalories, rating, isFavorite);

            return recipe;
        }
    }
}
