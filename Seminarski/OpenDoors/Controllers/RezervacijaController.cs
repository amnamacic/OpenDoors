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

            var rezervacija = new Rezervacija
            {
               BrojOsoba=x.BrojOsoba,
               Djeca=x.Djeca,
               Status=x.Status,
               CheckIn=x.CheckIn,
               CheckOut=x.CheckOut,
               Cijena=x.Cijena,
               NekretninaId=x.NekretninaId,
               KorisnikId=x.KorisnikId,
              
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

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}


