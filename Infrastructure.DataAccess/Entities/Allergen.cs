using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities
{
    public class Allergen
    {
        [Key]
        public Guid Primarykey { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Reaction { get; set; } = string.Empty;
        public int DangerDegree { get; set; } = 5;
    }
}
