using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Grad
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int PostanskiBroj { get; set; }
        public List<Lokacija> Lokacija { get; set; }
        public List<Korisnik> Korisnik { get; set; }
    }
}
