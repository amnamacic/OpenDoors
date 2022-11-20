using OpenDoors.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OpenDoors.Models
{
    public class Nekretnina
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public DateTime DatumIzmjene { get; set; }
        [ForeignKey("OpisId")]
        public OpisNekretnine Opis { get; set; }
        public int OpisId { get; set; }
        [ForeignKey("LokacijaId")]
        public Lokacija Lokacija { get; set; }
        public int LokacijaId { get; set; }
        [ForeignKey("TipId")]
        public TipNekretnine Tip { get; set; }
        public int TipId { get; set; }
        public List<Transakcija> Transakcija { get; set; }
        public List<Recenzije> Recenzije { get; set; }
        public List<Rezervacija> Rezervacija { get; set; }
        public List<PosebnaPonuda> PosebnaPonuda { get; set; }
        [ForeignKey("VlasnikId")]
        public Vlasnik Vlasnik { get; set; }
        public int VlasnikId { get; set; }
        public List<KrajnjiKorisnik> KrajnjiKorisnik { get; set; }
    }
}
