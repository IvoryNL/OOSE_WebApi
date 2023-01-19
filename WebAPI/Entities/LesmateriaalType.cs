using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class LesmateriaalType
    {
        public int Id { get; set; }

        [MaxLength(80)]
        public string Naam { get; set; }
    }
}
