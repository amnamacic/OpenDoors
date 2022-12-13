using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PromoKodoviController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PromoKodoviController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public PromoKodovi Snimi([FromBody] PromoKodoviAdd x)
        {

            var promoKodovi = new PromoKodovi
            {
                Opis = x.Opis,
                Popust = x.Popust,
                Naziv=x.Naziv,
                Kod=x.Kod,
            };

            _dbContext.Add(promoKodovi);
            _dbContext.SaveChanges();
            return promoKodovi;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.PromoKodovi
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    Opis = s.Opis,
                    Popust = s.Popust,
                    Naziv = s.Naziv,
                    Kod=s.Kod,
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}




