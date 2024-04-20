using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Entities
{
    public class Pacient
    {
        [Key]
        public Guid Primarykey { get; set; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Fio { get; set; } = string.Empty;


        public string Analysis { get; set; } = string.Empty;
        public string Сonclusion { get; set; } = string.Empty;
    }
}
