import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";


declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-detalji-nekretnine',
  templateUrl: './detalji-nekretnine.component.html',
  styleUrls: ['./detalji-nekretnine.component.css']
})
export class DetaljiNekretnineComponent implements OnInit {
  pogodnosti: any;
  nekretninaId: number;
  rezervacijaPodaci: any;
   recenzijePodaci: any;
   komentarPodaci: any;
  odabranaNekretnina: any;
  slike:any;
  objekat:any;
  noviKomentar: any;
  slika:any;
  constructor(private httpKlijent: HttpClient, private router: Router,  private route: ActivatedRoute,private formBuilder: FormBuilder) {
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  ngOnInit(): void {
    this.route.params.subscribe(s=>{
      this.nekretninaId=+s["id"];
      this.fetchPogodnosti();
    })
    console.log(this.loginInfo().autentifikacijaToken.korisnickiNalogId);
    this.slika =   {
      id:0,
      nekretninaId:this.nekretninaId,
      slika:""
    };

    this.fetchRecenzije();

    this.noviKomentar=this.formBuilder.group({
      komentar: new FormControl('', [
        Validators.required]),
      ocjena: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(10)
      ]),
      nekretninaId: new FormControl(this.nekretninaId),
      korisnickiNalogId: new FormControl(this.loginInfo().autentifikacijaToken.korisnickiNalog.id)
    });
  }

  get komentar() : FormControl{
    return this.noviKomentar.get("komentar") as FormControl;
  }
  get ocjena() : FormControl{
    return this.noviKomentar.get("ocjena") as FormControl;
  }

  fetchPogodnosti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Nekretnina/GetById/?nekretninaId="+this.nekretninaId, MojConfig.http_opcije()).subscribe(x => {
      this.pogodnosti = x;
    });

  }

  getslika(slika_id: any) {
      return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${slika_id}`;
  }
  generisi_preview() {
    // @ts-ignore
    var file = document.getElementById("slika-input").files[0];
    if (file) {
      var reader = new FileReader();
      let this2=this;
      reader.onload = function () {
        this2.slika.slika = reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }


  spasiSliku() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Slike/Snimi`, this.slika, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchPogodnosti();
    });
  }

  obrisiSliku(x: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Slike/Delete/${x}`, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchPogodnosti();
      porukaSuccess("Uspjesno ste izbrisali sliku!");
      this.slike=null;
    });
  }
  otvoriSlike(s:any) {
    this.slike=true;
    this.objekat=s;
  }

  rezervisiNekretninu(s:any) {
    this.router.navigate(['rezervacija/', s.id]);
  }

  urediNekretninu(s: any) {
    this.odabranaNekretnina=s;
  }
  otvoriRezervacije() {
    this.promjeniStatus();
       this.httpKlijent.get(MojConfig.adresa_servera + "/Rezervacija/GetById?nekretninaId=" + this.nekretninaId).
         subscribe((x:any)=>{
           this.rezervacijaPodaci=x;
       })
  }

  promjeniStatus(){
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Rezervacija/promjeniStatus?nekretninaId=`+this.nekretninaId, MojConfig.http_opcije()).subscribe(x => {
    });
  }
  fetchRecenzije() :void
  {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Recenzije/GetById?nekretninaId=" + this.nekretninaId,MojConfig.http_opcije()).subscribe(x=>{
      this.recenzijePodaci = x;
    });
  }

  ostaviKomentar() {
    if(this.noviKomentar.valid){
      this.httpKlijent.post(`${MojConfig.adresa_servera}/Recenzije/AddUpdate`, this.noviKomentar.value,MojConfig.http_opcije()).subscribe(x=>{
        this.fetchRecenzije();
        porukaSuccess("Uspjesno dodan komentar!");
      });
    }
    else
      console.table(this.noviKomentar.value)
  }

  urediKomentar(s:any) {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Recenzije/GetByKomentarId?komentarId=" + s.id,MojConfig.http_opcije()).subscribe(x=>{
      this.komentarPodaci=x;
    });
  }

  delete(s: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Delete/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchPogodnosti();
    });
  }

}
