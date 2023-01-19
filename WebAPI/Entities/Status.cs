using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }
    }
}
