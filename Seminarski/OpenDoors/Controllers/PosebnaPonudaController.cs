using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PosebnaPonudaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PosebnaPonudaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]

        public PosebnaPonuda Snimi([FromBody] PosebnaPonudaAdd x)
        {
            PosebnaPonuda? objekat;

            if (x.Id == 0)
            {
                objekat = new PosebnaPonuda();
                _dbContext.Add(objekat);//priprema sql
            }
            else
            {
                objekat = _dbContext.PosebnaPonuda.Find(x.Id);
            }

            objekat.Opis = x.Opis;
            objekat.Popust = x.Popust;
            objekat.NekretninaId = x.NekretninaId;

            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.PosebnaPonuda
                .OrderBy(s => s.Opis)
                .Select(s => new
                {
                    Opis = s.Opis,
                    Popust = s.Popust,
                    NekretninaId = s.NekretninaId,
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}



