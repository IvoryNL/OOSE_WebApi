namespace WebAPI.Entities
{
    public class Beoordeling
    {
        public int Id { get; set; }

        public int DocentId { get; set; }

        public int BeoordelingsmodelId { get; set; }

        public int? TentamenUploadId { get; set; }

        public int StatusId { get; set; }

        public DateTime Datum { get; set; }

        public decimal Resultaat { get; set; }

        public Gebruiker? Docent { get; set; }

        public Beoordelingsmodel? Beoordelingsmodel { get; set; }

        public Status? Status { get; set; }

        public TentamenVanStudent? TentamenUpload { get; set; }
    }
}
