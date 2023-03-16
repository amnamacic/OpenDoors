import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-detalji-nekretnine',
  templateUrl: './detalji-nekretnine.component.html',
  styleUrls: ['./detalji-nekretnine.component.css']
})
export class DetaljiNekretnineComponent implements OnInit {
  pogodnosti: any;
  nekretninaId: number;
  rezervacijaPodaci: any;

  constructor(private httpKlijent: HttpClient, private router: Router,  private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(s=>{
      this.nekretninaId=+s["id"];
      this.fetchPogodnosti();
    })
    this.slika =   {
      id:0,
      nekretninaId:this.nekretninaId,
      slika:""
    };
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

  slika:any;
  spasiSliku() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Slike/Snimi`, this.slika, MojConfig.http_opcije()).subscribe(x=>{
      this.fetchPogodnosti();
      this.slika=null;
    });
  }

  slike:any;
  objekat:any;
  otvoriSlike(s:any) {
    this.slike=true;
    this.objekat=s;
  }

  rezervisiNekretninu(s:any) {
    this.router.navigate(['rezervacija/', s.id]);
  }

  otvoriRezervacije() {
       this.httpKlijent.get(MojConfig.adresa_servera + "/Rezervacija/GetById?nekretninaId=" + this.nekretninaId).
         subscribe((x:any)=>{
           this.rezervacijaPodaci=x;
       })
  }
}
