namespace OpenDoors.ViewModels
{
    public class RezervacijaGetAll
    {
        public int BrojOsoba { get; set; }
        public int Djeca { get; set; }
        public string Status { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Cijena { get; set; }
        public string Nekretnina { get; set; }
        public string Korisnik { get; set; }
        public string KreditnaKartica {get; set; }

    }
}
