using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Rol
    {
        public int Id { get; set; }

        [MaxLength(15)]
        public string Naam { get; set; }
    }
}
