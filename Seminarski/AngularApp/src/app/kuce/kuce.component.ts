import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../app.module";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-kuce',
  templateUrl: './kuce.component.html',
  styleUrls: ['./kuce.component.css']
})
export class KuceComponent implements OnInit{
  kucePodaci: any;
  constructor(private httpKlijent: HttpClient, private router: Router) {
  }
  fetchKuce(s: any) {
    this.httpKlijent.get(`${MojConfig.adresa_servera}/Nekretnina/GetByTip?tipId=`+1).subscribe(x=>{
      this.kucePodaci=x;
    });
  }

  ngOnInit(): void {
    this.fetchKuce(1);
  }
}
