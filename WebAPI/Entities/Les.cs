using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Les
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Omschrijving { get; set; }

        public List<Planning>? Planningen { get; set; }

        public List<Leeruitkomst>? Leeruitkomsten { get; set; }

        public List<Lesmateriaal>? Lesmaterialen { get; set; }
    }
}
