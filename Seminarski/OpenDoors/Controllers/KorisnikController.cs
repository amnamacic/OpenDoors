using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using OpenDoors.Helper;
using OpenDoors.ViewModels;

namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public ActionResult PromijeniLozinku([FromBody] PromjenaLozinkeVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.id);
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

        [HttpGet("{korisnikid}")]
        public ActionResult GetSlikuKorisnika(int korisnikid)
        {
            byte[]? sKorisnika = _dbContext.Korisnik.Find(korisnikid).slikaKorisnika;


            return File(sKorisnika, "image/*");
        }

        [HttpPost]
        public ActionResult PromijeniSliku([FromBody] PromjenaSlikeVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.id);
            if (korisnik == null)
                return BadRequest();
            else
            {
                korisnik.slikaKorisnika = x.slikaKorisnika.ParsirajBase64();
                _dbContext.SaveChanges();
            }
            return Ok();
        }

    }
}
