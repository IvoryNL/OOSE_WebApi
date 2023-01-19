namespace WebAPI.Entities
{
    public class TentamenVanStudent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int TentamenId { get; set; }

        public string Bestand { get; set; }

        public DateTime Datum { get; set; }

        public Gebruiker Student { get; set; }

        public Beoordeling Beoordeling { get; set; }
    }
}
