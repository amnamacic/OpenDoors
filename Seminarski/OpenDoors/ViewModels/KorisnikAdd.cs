using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class KorisnikAdd
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public int GradId { get; set; }
    }
}
