using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    [Table("Korisnik")]
    
    public class Korisnik : KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        [ForeignKey("GradId")]
        public Grad Grad { get; set; }
        public int GradId { get; set; }
        public List<PromoKodovi> PromoKodovi { get; set; }
        public List<KreditnaKartica> KreditnaKartica { get; set; }
        public List<Rezervacija> Rezervacija { get; set; }
        public List<Transakcija> Transakcija { get; set; }
    }
}

