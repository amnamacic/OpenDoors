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
    public class GradController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GradController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Grad Snimi([FromBody] GradAdd x)
        {
            Grad? objekat;

            if (x.Id == 0)
            {
                objekat = new Grad();
                _dbContext.Add(objekat);//priprema sql
            }
            else
            {
                objekat = _dbContext.Grad.Find(x.Id);
            }

            objekat.Naziv = x.Naziv;
            objekat.PostanskiBroj = x.PostanskiBroj;

            _dbContext.SaveChanges();
            return objekat;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Grad
                .OrderBy(s => s.Naziv)
                .Select(s => new GradGetAll()
                {
                    id = s.Id,
                    postanskiBroj = s.PostanskiBroj,
                    naziv = s.Naziv,
                })
                .Take(100);
            return Ok(data.ToList());
        }
    }


}
