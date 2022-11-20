using OpenDoors.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Notifikacija
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }
        public bool Status { get; set; }
        public List<KorisnickiNalog> KorisnickiNalog { get; set; }
    }
}


