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

  constructor(private httpKlijent: HttpClient, private router: Router,  private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(s=>{
      this.nekretninaId=+s["id"];
      this.fetchPogodnosti();
    })

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
        this2.pogodnosti.slika_Nova = reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  slika:any;
  spasiSliku() {
    this.slika =   {
      id:0,
      nekretnina_id:this.nekretninaId,
      slika_Nova:""
    };
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Slike/Snimi`, this.slika, MojConfig.http_opcije()).subscribe(x=>{
    });
  }

  slike:any;
  objekat:any;
  otvoriSlike(s:any) {
    this.slike=true;
    this.objekat=s;
  }
}
