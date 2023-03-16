import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {NekretninaVM} from "./nekretninaVM";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

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
      vlasnikId: this.loginInfo().autentifikacijaToken.korisnickiNalog.id,
      slike:[],
      selectedPogodnosti:[]
    };

  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  fetchPogodnosti(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/PogodnostiNekretnine/GetAll`,MojConfig.http_opcije()).subscribe(data => {
      this.pogodnostiNekretnine = data;
    });
  }

  fetchLokacije(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Lokacija/GetAll`,MojConfig.http_opcije()).subscribe(data => {
      this.lokacije = data;
    });
  }

  fetchTipove(): void {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/TipNekretnine/GetAll`,MojConfig.http_opcije()).subscribe(data => {
      this.tipovi = data;
    });
  }

  addNekretnina() {
    this.novaNekretnina.selectedPogodnosti = this.pogodnostiNekretnine.filter((w:any)=>w.selected == true).map((s:any)=>s.id);
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Snimi`, this.novaNekretnina, MojConfig.http_opcije()).subscribe(x => {
      this.router.navigateByUrl("/pocetna");
    });
  }

  postaviNekretninu() {
    this.addNekretnina();
  }

  promjenaAvansa() {
    this.novaNekretnina.avans = true;
  }


  randomIntFromInterval(min: number, max: number) { // min and max included
    return Math.floor(Math.random() * (max - min + 1) + min)
  }

  get_slika(s: string) {
    return s;
  }

  generisi_preview() {
    // @ts-ignore
    let files = document.getElementById("slika-input").files;
    if (files.length>0) {
      for (let file of files) {
        let reader = new FileReader();
        let this2 = this;
        reader.onload = function () {
          this2.novaNekretnina.slike.push(reader.result.toString());
        }

        reader.readAsDataURL(file);
      }

    }
  }


}
