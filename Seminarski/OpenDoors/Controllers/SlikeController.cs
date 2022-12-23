using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Helper;

namespace OpenDoors.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SlikeController : ControllerBase   
    {
        private readonly ApplicationDbContext _dbContext;
        public SlikeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] SlikeGetAll x)
        {
            Slike? slika;
            slika = new Slike
            {
                NekretninaId = x.NekretninaId,
                DatumPostavljanja = DateTime.Now,
            };


            if (x.Slika != "")
            {
                //slika se snima u db
                byte[] slika_bajtovi = x.Slika.ParsirajBase64();
                slika.Slika = slika_bajtovi;
            }

            _dbContext.Add(slika);//priprema sql
            _dbContext.SaveChanges();
            return Ok();
        }



        [HttpGet("{id}")]
        public ActionResult GetSlikaDB(int id)
        {
            byte[] bajtovi_slike;

            if (_dbContext.Slike.FirstOrDefault(x => x.NekretninaId == id)==null || 
                _dbContext.Slike.FirstOrDefault(x => x.NekretninaId == id).Slika == null)
            {
                bajtovi_slike = Fajlovi.Ucitaj("wwwroot/images/download.png");
            }
            else
            {
                bajtovi_slike = _dbContext.Slike.FirstOrDefault(x => x.NekretninaId == id).Slika;
            }

            return File(bajtovi_slike, "image/png");
        }
    }
}
