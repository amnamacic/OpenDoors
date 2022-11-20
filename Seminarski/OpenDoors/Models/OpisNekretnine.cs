using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class OpisNekretnine
    {
        [Key]
        public int Id { get; set; }
        public int BrojKvadrata { get; set; }
        public int BrojSoba { get; set; }
        public int BrojKupatila { get; set; }
        public int BrojKreveta { get; set; }
        public double CijenaPoDanu { get; set; }
        public string Adresa { get; set; }
        public bool Avans { get; set; }
        public List<PogodnostiNekretnine> PogodnostiNekretnine { get; set; }
        public List<Slike> Slike { get; set; }
    }
}