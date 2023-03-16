using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;

namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RezervacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Rezervacija Snimi([FromBody] RezervacijaAdd x)
        {
            var cijenaPoDanu = _dbContext.Nekretnina.Find(x.NekretninaId).CijenaPoDanu;

            var rezervacija = new Rezervacija
            {
               BrojOsoba=x.BrojOsoba,
               Djeca=x.Djeca,
               Status="Aktivna",
               CheckIn=x.CheckIn,
               CheckOut=x.CheckOut,
               Cijena= (x.CheckOut.Subtract(x.CheckIn)).Days*cijenaPoDanu ,
               NekretninaId=x.NekretninaId,
               KorisnikId=x.KorisnikId,
               KreditnaKarticaId=x.KreditnaKarticaId
            };

            _dbContext.Add(rezervacija);
            _dbContext.SaveChanges();
            return rezervacija;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Rezervacija
                .OrderBy(s => s.NekretninaId)
                .Select(s => new
                {
                    BrojOsoba = s.BrojOsoba,
                    Djeca = s.Djeca,
                    Status = s.Status,
                    CheckIn = s.CheckIn,
                    CheckOut = s.CheckOut,
                    Cijena = s.Cijena,
                    NekretninaId = s.NekretninaId,
                    KorisnikId = s.KorisnikId,
                    KreditnaKartica=s.KreditnaKartica.TipKartice + s.KreditnaKartica.BrojKartice,
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
        [HttpGet]
        public List<RezervacijaGetAll> GetById(int nekretninaId)
        {
            
            var data = _dbContext.Rezervacija.Where(x => x.NekretninaId == nekretninaId)
                .OrderBy(s => s.Id)
                .Select(s => new RezervacijaGetAll
                {
                    BrojOsoba = s.BrojOsoba,
                    Djeca = s.Djeca,
                    Status = s.Status,
                    CheckIn = s.CheckIn,
                    CheckOut = s.CheckOut,
                    Cijena = s.Cijena,
                    Nekretnina = s.Nekretnina.Adresa+s.Nekretnina.Lokacija.Naziv,
                    Korisnik = s.Korisnik.Ime+s.Korisnik.Prezime,
                    KreditnaKartica = s.KreditnaKartica.TipKartice + s.KreditnaKartica.BrojKartice,
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }
    }
}


