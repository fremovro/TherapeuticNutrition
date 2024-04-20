using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities.Relations
{
    public class PacientAllergen
    {
        [Key]
        public Guid Primarykey { get; set; }

        public Guid Pacient { get; set; }
        public Guid Allergen { get; set; }
    }
}
