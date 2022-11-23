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

            var nekretnina = new Nekretnina
            {
                BrojKvadrata = x.BrojKvadrata,
                BrojKupatila = x.BrojKupatila,
                BrojSoba = x.BrojSoba,
                BrojKreveta = x.BrojKreveta,
                Avans = x.Avans,
                Adresa = x.Adresa,
                CijenaPoDanu = x.CijenaPoDanu,
                LokacijaId = x.LokacijaId,
                TipId = x.TipId,
                VlasnikId = x.VlasnikId,
            };

            _dbContext.Add(nekretnina);
            _dbContext.SaveChanges();
            return nekretnina;
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

