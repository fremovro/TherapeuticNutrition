using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities
{
    public class Recipe
    {
        [Key]
        public Guid Primarykey { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Сalories { get; set; }
        public decimal Rating { get; set; }
    }
}
