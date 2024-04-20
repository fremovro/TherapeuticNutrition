using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities.Relations
{
    public class RecipeIngredient
    {
        [Key]
        public Guid Primarykey { get; set; }

        public Guid Recipe { get; set; }
        public Guid Product { get; set; }
    }
}
