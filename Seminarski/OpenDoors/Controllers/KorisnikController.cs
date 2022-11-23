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
    public class KorisnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Korisnik Snimi([FromBody] KorisnikAdd x)
        {

            var korisnik = new Korisnik
            {
               Ime=x.Ime,
               Prezime=x.Prezime,
               Spol=x.Spol,
               DatumRodjenja=x.DatumRodjenja,
               BrojTelefona=x.BrojTelefona,
               GradId=x.GradId
            };

            _dbContext.Add(korisnik);
            _dbContext.SaveChanges();
            return korisnik;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Korisnik
                .OrderBy(s => s.Ime)
                .Select(s => new
                {
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Spol = s.Spol,
                    DatumRodjenja = s.DatumRodjenja,
                    BrojTelefona = s.BrojTelefona,
                    GradId = s.GradId

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}



