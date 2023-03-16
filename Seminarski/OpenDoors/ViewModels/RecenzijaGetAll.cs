namespace OpenDoors.ViewModels
{
    public class RecenzijaGetAll
    {
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        public int NekretninaId { get; set; }
        public string Korisnik { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public DateTime? DatumModifikacije { get; set; }
    }
}
