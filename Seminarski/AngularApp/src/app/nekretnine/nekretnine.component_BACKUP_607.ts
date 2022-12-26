import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {MojConfig} from "../../MojConfig";

@Component({
  selector: 'app-nekretnine',
  templateUrl: './nekretnine.component.html',
  styleUrls: ['./nekretnine.component.css']
})
export class NekretnineComponent implements OnInit {
  nekretninaPodaci: any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  getNekretnine(): void {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Nekretnina/GetAll", MojConfig.http_opcije()).subscribe(x => {
      this.nekretninaPodaci = x;
    });
  }

  ngOnInit(): void {
    this.getNekretnine();
  }

  detaljiNekretnine(s: any) {
    this.router.navigate(["detalji-nekretnine/", s.id]);
  }

  getslika(slika_id: any) {
    return `${MojConfig.adresa_servera}/Slike/GetSlikaDB/${slika_id}`;
  }

  delete(s: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Nekretnina/Delete/${s.id}`, MojConfig.http_opcije()).subscribe(x => {
      this.getNekretnine();
    });
  }
}
