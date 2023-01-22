namespace WebAPI.Entities
{
    public class Planning
    {
        public int Id { get; set; }

        public int OnderwijsuitvoeringId { get; set; }

        public DateTime Datum { get; set; }

        public int Weeknummer { get; set; }

        public Onderwijsuitvoering? Onderwijsuitvoering { get; set; }

        public List<Tentamen>? Tentamens { get; set; }

        public List<Les>? Lessen { get; set; }
    }
}
