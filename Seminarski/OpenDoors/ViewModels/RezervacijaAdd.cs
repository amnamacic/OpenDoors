using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class RezervacijaAdd
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
