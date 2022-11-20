using OpenDoors.Models;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Models;

namespace OpenDoors.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Email> Email { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<KreditnaKartica> KreditnaKartica { get; set; }
        public DbSet<Lokacija> Lokacija { get; set; }
       


        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

    }
}







