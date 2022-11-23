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

            var posebnaPonuda = new PosebnaPonuda
            {
               Opis=x.Opis,
               Popust=x.Popust,
               NekretninaId=x.NekretninaId,
            };

            _dbContext.Add(posebnaPonuda);
            _dbContext.SaveChanges();
            return posebnaPonuda;
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



