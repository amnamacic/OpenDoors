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
    public class KrajnjiKorisnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KrajnjiKorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public KrajnjiKorisnik Snimi([FromBody] KrajnjiKorisnikAdd x)
        {

            var korisnik = new KrajnjiKorisnik
            {
               Ime=x.Ime,
               Prezime=x.Prezime,
               Spol=x.Spol,
               GodinaRodjenja=x.GodinaRodjenja,
               BrojTelefona=x.BrojTelefona,
               Id=x.Id,
               Username=x.Username,
               Password=x.Password,
               Email=x.Email
            };

            _dbContext.Add(korisnik);
            _dbContext.SaveChanges();
            return korisnik;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KrajnjiKorisnik
                .OrderBy(s => s.Ime)
                .Select(s => new KrajnjiKorisnikGetAll()
                {
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Spol = s.Spol,
                    GodinaRodjenja = s.GodinaRodjenja,
                    BrojTelefona = s.BrojTelefona,
                    Id = s.Id,
                    Username = s.Username,
                    Password = s.Password,
                    Email = s.Email
                })
                .Take(100);
            return Ok(data.ToList());
        }

        [HttpPost]
        public ActionResult PromjeniLozinku([FromBody] PromjenaLozinkeVM x)
        {
            var korisnik = _dbContext.KrajnjiKorisnik.Find(x.id);
            if (korisnik == null)
                return BadRequest();
            else if (korisnik.Password != x.staraLozinka)
                return BadRequest();
            else
            {
                korisnik.Password = x.novaLozinka;
                _dbContext.SaveChanges();
            }

            return Ok();
        }
    }


}

