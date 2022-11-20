using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Slike
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public byte[] Slika { get; set; }
       [ForeignKey("OpisId")]
        public OpisNekretnine OpisNekretnine { get; set; }
        public int OpisId { get; set; }
    }
}
