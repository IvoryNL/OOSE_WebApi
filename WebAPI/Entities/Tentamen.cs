using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Tentamen
    {
        public int Id { get; set; }

        public int VormId { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        public Vorm? Vorm { get; set; }

        public List<Planning>? Planningen { get; set; }

        public Beoordelingsmodel? Beoordelingsmodel { get; set; }

        public List<Leeruitkomst>? Leeruitkomsten { get; set; }
    }
}
