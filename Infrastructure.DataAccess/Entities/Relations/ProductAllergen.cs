using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities.Relations
{
    public class ProductAllergen
    {
        [Key]
        public Guid Primarykey { get; set; }

        public Guid Product { get; set; }
        public Guid Allergen { get; set; }
    }
}
