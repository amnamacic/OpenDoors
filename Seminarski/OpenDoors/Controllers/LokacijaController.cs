using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class LokacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public LokacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Lokacija Snimi([FromBody] LokacijaAdd x)
        {

            var lokacija = new Lokacija
            {
                DioGrada = x.DioGrada,
                Naziv = x.Naziv,
                GradId = x.GradId
            };

            _dbContext.Add(lokacija);
            _dbContext.SaveChanges();
            return lokacija;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Lokacija
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    naziv = s.Naziv,
                    dioGrada=s.DioGrada,
                    gradID=s.GradId
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }


}

