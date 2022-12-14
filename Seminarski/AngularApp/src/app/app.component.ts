import { Component, OnInit } from '@angular/core';
import {AppService} from "./get-api.service";
import {MojConfig} from "./app.module";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  NekretninaPodaci: any;
  title = 'OpenDoors';
  router: any;


  constructor(private httpKlijent: AppService) {
    this.NekretninaPodaci = [];
  }

  ngOnInit(): void {
    this.httpKlijent.apiCall().subscribe((data) => {
      this.NekretninaPodaci = data
    })
  }


}




