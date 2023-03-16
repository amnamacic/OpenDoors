using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;

namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecenzijeController: ControllerBase 
    {
        private readonly ApplicationDbContext _dbContext;

        public RecenzijeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Recenzije AddUpdate([FromBody] RecenzijaAdd x)
        {
            Recenzije? recenzija;

            if (x.Id == 0)
            {
                recenzija = new Recenzije();
                _dbContext.Add(recenzija);
            }
            else
            {
                recenzija = _dbContext.Recenzije.Find(x.Id);
            }

            recenzija.Ocjena = x.Ocjena;
            recenzija.Komentar = x.Komentar;
            recenzija.NekretninaId = x.NekretninaId;
            recenzija.KorisnickiNalogId = x.KorisnickiNalogId;

            _dbContext.SaveChanges();
            return recenzija;
        }

        [HttpGet]
        public List<RecenzijaGetAll> GetById(int nekretninaId)
        {

            var data = _dbContext.Recenzije.Where(x => x.NekretninaId == nekretninaId)
                .OrderBy(s => s.Id)
                .Select(s => new RecenzijaGetAll
                {
                    Ocjena=s.Ocjena,
                    Komentar=s.Komentar,
                    Korisnik=s.KorisnickiNalog.Username,
                    DatumModifikacije=s.DatumModifikacije,
                    DatumPostavljanja=s.DatumPostavljanja
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }
    }
}
