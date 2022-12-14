﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Data;
using OpenDoors.Helper;
using OpenDoors.Models;

namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TestniPodaciController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Vlasnik", _dbContext.Vlasnik.Count());
            data.Add("KrajnjiKorisnik", _dbContext.KrajnjiKorisnik.Count());
            data.Add("Grad", _dbContext.Grad.Count());
            data.Add("Nekretnina", _dbContext.Nekretnina.Count());
            data.Add("Rezervacija", _dbContext.Rezervacija.Count());
            data.Add("TipNekretnine", _dbContext.TipNekretnine.Count());
            data.Add("Lokacija", _dbContext.TipNekretnine.Count());


            return Ok(data);
        }

        [HttpPost]
        public ActionResult Generisi()
        {
            var vlasnici = new List<Vlasnik>();
            var krajnjiKorisnici = new List<KrajnjiKorisnik>();
            var gradovi = new List<Grad>();
            var nekretnine = new List<Nekretnina>();
            var rezervacije = new List<Rezervacija>();
            var tipoviNekretnina = new List<TipNekretnine>();
            var lokacije = new List<Lokacija>();

          
            vlasnici.Add(new Vlasnik { Ime = "Asmira", Prezime = "Husić", Spol = "Ž", DatumRodjenja = DateTime.Now, BrojTelefona = "062/555-111", GradId = 2, Username="asmira.husic", Password="Asmira123", Email="asmira.husic@edu.fit.ba" });
            vlasnici.Add(new Vlasnik { Ime = "Džana", Prezime = "Boloban", Spol = "Ž", DatumRodjenja = DateTime.Now, BrojTelefona = "062/555-222", GradId = 2, Username="dzana.boloban", Password="Dzana123", Email="dzana.boloban@edu.fit.ba" });

            krajnjiKorisnici.Add(new KrajnjiKorisnik { Ime = "Amna", Prezime = "Macić", Spol = "Ž", DatumRodjenja = DateTime.Now, BrojTelefona = "062/555-666", GradId = 1, Username = "amna.macic", Password = "Amna123", Email = "amna.macic@edu.fit.ba" });
            krajnjiKorisnici.Add(new KrajnjiKorisnik { Ime = "Amina", Prezime = "Muhibić", Spol = "Ž", DatumRodjenja = DateTime.Now, BrojTelefona = "062/555-000", GradId = 1, Username = "amina.muhibic", Password = "Amina123", Email = "amina.muhibic@edu.fit.ba" });
        

            gradovi.Add(new Grad {Naziv = "Mostar", PostanskiBroj=88000 });
            gradovi.Add(new Grad { Naziv = "Konjic", PostanskiBroj = 88400 });
            gradovi.Add(new Grad { Naziv = "Sarajevo", PostanskiBroj = 71000 });

            nekretnine.Add(new Nekretnina { Status=true, DatumPostavljanja=DateTime.Now, BrojKvadrata=80, BrojKreveta=16, BrojSoba=8, BrojKupatila=4, CijenaPoDanu=50, Adresa="Prkanj bb", Avans=true, VlasnikId=2, LokacijaId=1, TipId=1 });
            nekretnine.Add(new Nekretnina { Status = true, DatumPostavljanja = DateTime.Now, BrojKvadrata = 60, BrojKreveta = 8, BrojSoba = 2, BrojKupatila = 2, CijenaPoDanu = 50, Adresa = "Orasje bb", Avans = true, VlasnikId = 2, LokacijaId = 1, TipId = 1 });

            rezervacije.Add(new Rezervacija { DatumRezervacije = DateTime.Now, BrojOsoba = 5, Djeca = true, Status = "Zavrsena", DatumOtkazivanja = DateTime.Now, Cijena = 100, CheckIn = DateTime.Now, CheckOut =DateTime.Now, PovratNovca = true, NekretninaId = 2, KorisnikId = 1});
            rezervacije.Add(new Rezervacija { DatumRezervacije = DateTime.Now, BrojOsoba = 3, Djeca = false, Status = "U toku", DatumOtkazivanja = DateTime.Now, Cijena = 80, CheckIn = DateTime.Now, CheckOut = DateTime.Now, PovratNovca = true, NekretninaId = 2, KorisnikId = 1 });

            tipoviNekretnina.Add(new TipNekretnine { Tip="Kuća", Opis="..." });
            tipoviNekretnina.Add(new TipNekretnine { Tip="Stan", Opis="..."});

            lokacije.Add(new Lokacija { DioGrada="Centar", Naziv="Musala", GradId=2});
            lokacije.Add(new Lokacija { DioGrada = "Centar", Naziv = "Bulevar", GradId =2});

            Random rnd = new Random();

           

            _dbContext.AddRange(nekretnine);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(lokacije);
            _dbContext.AddRange(krajnjiKorisnici);
            _dbContext.AddRange(vlasnici);
            _dbContext.AddRange(tipoviNekretnina);
            _dbContext.AddRange(rezervacije);
            _dbContext.SaveChanges();

            return Count();
        }
    }
}