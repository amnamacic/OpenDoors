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
    public class NekretninaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public NekretninaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]

        public Nekretnina Snimi([FromBody] NekretninaAdd x)
        {
            Nekretnina? objekat;

            if (x.Id == 0)
            {
                objekat = new Nekretnina();
                _dbContext.Add(objekat);//priprema sql
            }
            else
            {
                objekat = _dbContext.Nekretnina.Find(x.Id);
            }

            objekat.BrojSoba = x.BrojSoba;
            objekat.BrojKupatila = x.BrojKupatila;
            objekat.BrojKvadrata = x.BrojKvadrata;
            objekat.Adresa = x.Adresa;
            objekat.Avans = x.Avans;
            objekat.BrojKreveta = x.BrojKreveta;
            objekat.CijenaPoDanu = x.CijenaPoDanu;
            objekat.LokacijaId = x.LokacijaId;
            objekat.TipId = x.TipId;
            objekat.VlasnikId = x.VlasnikId;

            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Nekretnina
                .OrderBy(s => s.Adresa)
                .Select(s => new
                {
                    brojKvadrata = s.BrojKvadrata,
                    brojKupatila = s.BrojKupatila,
                    brojSoba = s.BrojSoba,
                    brojKreveta = s.BrojKreveta,
                    avans = s.Avans,
                    adresa = s.Adresa,
                    cijenaPoDanu = s.CijenaPoDanu,
                    lokacijaId = s.LokacijaId,
                    tipId = s.TipId,
                    vlasnikId = s.VlasnikId,
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}

