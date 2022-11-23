namespace OpenDoors.ViewModels
{
    public class RezervacijaGetAll
    {
        public int BrojOsoba { get; set; }
        public bool Djeca { get; set; }
        public string Status { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Cijena { get; set; }
        public int NekretninaId { get; set; }
        public int KorisnikId { get; set; }
    }
}
