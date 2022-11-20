using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IznajmljivanjeNekretnina.Models
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Verifikovan { get; set; }
        public List<Recenzije> Recenzije { get; set; }
        public List<Notifikacija> Notifikacija { get; set; }
        public List<Email> Emails { get; set; }

    }
}

