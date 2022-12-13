using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class VlasnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public VlasnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Vlasnik Snimi([FromBody] VlasnikAdd x)
        {

            var vlasnik = new Vlasnik
            {
                Ime = x.Ime,
                Prezime = x.Prezime,
                Spol = x.Spol,
                DatumRodjenja = x.DatumRodjenja,
                BrojTelefona = x.BrojTelefona,
                GradId = x.GradId,
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email
            };

            _dbContext.Add(vlasnik);
            _dbContext.SaveChanges();
            return vlasnik;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Vlasnik
                .OrderBy(s => s.Ime)
                .Select(s => new VlasnikGetAll()
                {
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Spol = s.Spol,
                    DatumRodjenja = s.DatumRodjenja,
                    BrojTelefona = s.BrojTelefona,
                    GradId = s.GradId,
                    Id = s.Id,
                    Username = s.Username,
                    Password = s.Password,
                    Email = s.Email
                })
                .Take(100);
            return Ok(data.ToList());
        }
    }


}


