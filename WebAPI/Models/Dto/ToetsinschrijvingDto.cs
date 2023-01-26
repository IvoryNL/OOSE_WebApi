using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class ToetsinschrijvingDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int TentamenId { get; set; }

        public int PlanningId { get; set; }

        public VolledigeGebruikerModelDto? Student { get; set; }

        public Tentamen? Tentamen { get; set; }

        public Planning? Planning { get; set; }
    }
}
