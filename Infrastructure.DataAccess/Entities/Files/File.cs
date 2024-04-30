using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities.Files
{
    public class File
    {
        [Key]
        public Guid Primarykey { get; set; }

        public Guid Relation { get; set; }
        public string? Content { get; set; }
    }
}
