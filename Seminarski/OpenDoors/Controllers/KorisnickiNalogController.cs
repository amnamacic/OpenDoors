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
    public class KorisnickiNalogController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnickiNalogController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public KorisnickiNalog Snimi([FromBody] KorisnickiNalogAdd x)
        {

            var korisnickiNalog = new KorisnickiNalog
            {
                Username=x.Username,
                Password=x.Password,
                Email=x.Email,
            };

            _dbContext.Add(korisnickiNalog);
            _dbContext.SaveChanges();
            return korisnickiNalog;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KorisnickiNalog
                .OrderBy(s => s.Username)
                .Select(s => new
                {
                    Username = s.Username,
                    Password = s.Password,
                    Email = s.Email,

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}



