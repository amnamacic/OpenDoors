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

  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*'
    })
  };

  getNekretnine() :void
      {
        this.httpKlijent.get(MojConfig.adresa_servera+ "/Nekretnina/GetAll",this.httpOptions).subscribe(x=>{
          this.nekretninaPodaci = x;
        });
  }
  ngOnInit() :void{
    this.getNekretnine();
  }

  detaljiNekretnine(s:any) {
    this.router.navigate(["detalji-nekretnine/",s.id])
  }
}
