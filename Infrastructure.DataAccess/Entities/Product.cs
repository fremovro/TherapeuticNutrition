using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities
{
    public class Product
    {
        [Key]
        public Guid Primarykey { get; set; }

        public string Name { get; set; } = string.Empty;
        public string NutritionalValue { get; set; } = string.Empty;
    }
}
