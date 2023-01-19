namespace WebAPI.Entities
{
    public class Toetsinschrijving
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int TentamenId { get; set; }

        public int PlanningId { get; set; }

        public Gebruiker Student { get; set; }

        public Tentamen Tentamen { get; set; }

        public Planning Planning { get; set; }
    }
}
