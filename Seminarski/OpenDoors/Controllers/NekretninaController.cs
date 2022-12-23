﻿using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class NekretninaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public NekretninaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]

        public Nekretnina Snimi([FromBody] NekretninaAdd x)
        {
            Nekretnina? objekat;

            if (x.Id == 0)
            {
                objekat = new Nekretnina();
                _dbContext.Add(objekat);//priprema sql
            }
            else
            {
                objekat = _dbContext.Nekretnina.Find(x.Id);
            }

            objekat.BrojSoba = x.BrojSoba;
            objekat.BrojKupatila = x.BrojKupatila;
            objekat.BrojKvadrata = x.BrojKvadrata;
            objekat.Adresa = x.Adresa;
            objekat.Avans = x.Avans;
            objekat.BrojKreveta = x.BrojKreveta;
            objekat.CijenaPoDanu = x.CijenaPoDanu;
            objekat.LokacijaId = x.LokacijaId;
            objekat.TipId = x.TipId;
            objekat.VlasnikId = x.VlasnikId;

            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Nekretnina
                .OrderBy(s => s.Adresa)
                .Select(s => new
                {
                    id=s.Id,
                    brojKvadrata = s.BrojKvadrata,
                    brojKupatila = s.BrojKupatila,
                    brojSoba = s.BrojSoba,
                    brojKreveta = s.BrojKreveta,
                    avans = s.Avans,
                    adresa = s.Adresa,
                    cijenaPoDanu = s.CijenaPoDanu,
                    lokacijaId = s.LokacijaId,
                    lokacija=s.Lokacija,
                    tip=s.Tip,
                    tipId = s.TipId,
                    vlasnikId = s.VlasnikId,
                    vlasnik=s.Vlasnik.Ime+" "+s.Vlasnik.Prezime
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public List<NekretninaGetAll> GetByTip(int tipId)
        {
            var data = _dbContext.Nekretnina.Where(x => x.TipId == tipId)
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata =s.BrojKvadrata,
                    BrojKupatila=s.BrojKupatila,
                    BrojSoba=s.BrojSoba,
                    BrojKreveta=s.BrojKvadrata,
                    Adresa=s.Adresa,
                    CijenaPoDanu=s.CijenaPoDanu,
                    Avans=s.Avans,
                    LokacijaId=s.LokacijaId,
                    Lokacija=s.Lokacija.DioGrada,
                    Tip=s.Tip.Opis,
                    VlasnikId=s.VlasnikId,
                    TipId=s.TipId
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]
        public List<NekretninaGetAll> GetById(int nekretninaId)
        {
            var pogodnosti = _dbContext.NekretninaPogodnostiNekretnine.Where(x => x.NekretninaId == nekretninaId);
            var data = _dbContext.Nekretnina.Where(x => x.Id == nekretninaId)
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata = s.BrojKvadrata,
                    BrojKupatila = s.BrojKupatila,
                    BrojSoba = s.BrojSoba,
                    BrojKreveta = s.BrojKvadrata,
                    Adresa = s.Adresa,
                    CijenaPoDanu = s.CijenaPoDanu,
                    Avans = s.Avans,
                    LokacijaId = s.LokacijaId,
                    Lokacija=s.Lokacija.DioGrada,
                    VlasnikId = s.VlasnikId,
                    ImeVlasnik=s.Vlasnik.Ime+ " " + s.Vlasnik.Prezime,
                    TipId = s.TipId,
                    Tip=s.Tip.Opis,
                    Pogodnosti=pogodnosti.Select(x=>x.PogodnostiNekretnine.Naziv).ToList()
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }
    }
}

