using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Opleiding
    {
        public int Id { get; set; }

        public int VormId { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        public Vorm? Vorm { get; set; }

        public List<Opleidingsprofiel>? Opleidingsprofielen { get; set; }

        public List<Onderwijsmodule>? Onderwijsmodules { get; set; }
    }
}
