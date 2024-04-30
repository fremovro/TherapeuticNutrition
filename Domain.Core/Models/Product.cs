using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Models
{
    public class Product
    {
        private Product(Guid primarykey, string name, string nutritionalValue, bool isFavorite)
        {
            Primarykey = primarykey;
            Name = name;
            NutritionalValue = nutritionalValue;
            IsFavorite = isFavorite;
        }

        public Guid Primarykey { get; }

        public string Name { get; }
        public string NutritionalValue { get; }
        public bool IsFavorite { get; set; }

        public static Product CreateProduct(Guid primarykey, string name, string nutritionalValue, bool isFavorite = false)
        {
            var product = new Product(primarykey, name, nutritionalValue, isFavorite);

            return product;
        }
    }
}
