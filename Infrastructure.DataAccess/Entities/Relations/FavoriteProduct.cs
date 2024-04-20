using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities.Relations
{
    public class FavoriteProduct
    {
        [Key]
        public Guid Primarykey { get; set; }

        public Guid Pacient { get; set; }
        public Guid Product { get; set; }
    }
}
