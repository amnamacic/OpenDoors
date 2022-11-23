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
    public class KreditnaKarticaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KreditnaKarticaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public KreditnaKartica Snimi([FromBody] KreditnaKarticaAdd x)
        {

            var kreditnaKartica = new KreditnaKartica
            {
               BrojKartice=x.BrojKartice,
               TipKartice=x.TipKartice,
               KorisnikId=x.KorisnikId,
            };

            _dbContext.Add(kreditnaKartica);
            _dbContext.SaveChanges();
            return kreditnaKartica;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KreditnaKartica
                .OrderBy(s => s.TipKartice)
                .Select(s => new
                {
                    BrojKartice = s.BrojKartice,
                    TipKartice = s.TipKartice,
                    KorisnikId = s.KorisnikId,

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}



