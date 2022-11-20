using OpenDoors.Models;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Models;
using System.Collections.Generic;

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
        public DbSet<KrajnjiKorisnik> KrajnjiKorisnik { get; set; }
        public DbSet<Nekretnina> Nekretnina { get; set; }
        public DbSet<Notifikacija> Notifikacija { get; set; }
        public DbSet<OpisNekretnine> OpisNekretnine { get; set; }
        public DbSet<PogodnostiNekretnine> PogodnostiNekretnine { get; set; }
        public DbSet<PosebnaPonuda> PosebnaPonuda { get; set; }



        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

    }
}







