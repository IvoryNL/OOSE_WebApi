using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Leerdoel
    {
        public int Id { get; set; }

        public int OnderwijseenheidId { get; set; }

        [MaxLength(150)]
        public string Beschrijving { get; set; }

        public Onderwijseenheid? Onderwijseenheid { get; set; }

        public List<Leeruitkomst>? Leeruitkomsten { get; set; }
    }
}
