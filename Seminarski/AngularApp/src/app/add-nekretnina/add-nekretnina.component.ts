import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {NekretninaVM} from "./nekretninaVM";

@Component({
  selector: 'app-add-nekretnina',
  templateUrl: './add-nekretnina.component.html',
  styleUrls: ['./add-nekretnina.component.css']
})
export class AddNekretninaComponent implements OnInit {
  pogodnostiNekretnine: any;
  novaNekretnina: NekretninaVM;
  lokacije: any;
  tipovi: any;
  Slika: any;
  slika:string;
  selectedPogodnosti: any = [];

  constructor(private httpKlijent: HttpClient, private router: Router) {

  }

  ngOnInit() {
    this.fetchPogodnosti();
    this.fetchTipove();
    this.fetchLokacije();

    this.novaNekretnina = {
      id: 0,
      brojKvadrata: 0,
      brojSoba: 0,
      brojKupatila: 0,
      brojKreveta: 0,
      cijenaPoDanu: 0,
      adresa: " ",
      avans: false,
      lokacijaId: 0,
      GradOpis: " ",
      tipId: 1,
      lokacijaOpis: " ",
      tip: " ",
      vlasnikId: 1,
    };
    this.Slika={
      id: 0,
      nekretninaId: 12,
      slika: ""
    }
  }

  fetchPogodnosti(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/PogodnostiNekretnine/GetAll`).subscribe(data => {
      this.pogodnostiNekretnine = data;
    });
  }

  fetchLokacije(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Lokacija/GetAll`).subscribe(data => {
      this.lokacije = data;
    });
  }

  fetchTipove(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/TipNekretnine/GetAll`).subscribe(data => {
      this.tipovi = data;
    });
  }

  addNekretnina() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Snimi`, this.novaNekretnina, MojConfig.http_opcije()).subscribe(x => {
      this.router.navigateByUrl("/pocetna");
    });
  }

  postaviNekretninu() {
    this.addNekretnina();
    this.postaviSliku();
  }

  promjenaAvansa() {
    this.novaNekretnina.avans = true;
  }

  onSelectPogodnost(object: any) {
    if (object.selected) {
      this.selectedPogodnosti.push(object);
    }
    console.log(this.selectedPogodnosti);
  }
  randomIntFromInterval(min: number, max: number) { // min and max included
    return Math.floor(Math.random() * (max - min + 1) + min)
  }

  get_slika(s: any) {
    let r = this.randomIntFromInterval(1, 6);
    return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${s.id}?a=${r}`;
  }

  generisi_preview() {
    // @ts-ignore
    var file = document.getElementById("slika-input").files[0];
    if (file) {
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        this2.slika = reader.result.toString();
      }

      reader.readAsDataURL(file);
    }
  }
  postaviSliku() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Slike/Snimi`, this.Slika, MojConfig.http_opcije()).subscribe(x => {
    });
  }

}
