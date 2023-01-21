using System.ComponentModel.DataAnnotations;

namespace WebAPI.Entities
{
    public class Lesmateriaal
    {
        public int Id { get; set; }

        public int AuteurId { get; set; }

        public int LesmateriaaltypeId { get; set; }

        [MaxLength(50)]
        public string Naam { get; set; }

        [MaxLength(150)]
        public string Omschrijving { get; set; }

        public bool Verplicht { get; set; }

        public Auteur Auteur { get; set; }

        public LesmateriaalType LesmateriaalType { get; set; }

        public List<Les> Lessen { get; set; }

        public List<LesmateriaalInhoud> LesmateriaalInhoud { get; set; }
    }
}
