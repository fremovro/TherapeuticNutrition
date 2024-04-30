using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities.Relations
{
    public class FavoriteRecipe
    {
        [Key]
        public Guid Primarykey { get; set; }

        public Guid Pacient { get; set; }
        public Guid Recipe { get; set; }
    }
}
