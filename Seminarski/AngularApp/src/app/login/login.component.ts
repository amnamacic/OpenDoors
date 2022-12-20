import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../app.module";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {LoginInformacije} from "../helper/login-informacije";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  txtLozinka="asmira123";
  txtKorisnickoIme="asmira.husic";
  odabraniKorisnik:any;
  korisnikPodaci:any;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
  }

  btnLogin() {
    let saljemo = {
      korisnickoIme:this.txtKorisnickoIme,
      lozinka: this.txtLozinka
    };
    this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera+ "/Autentifikacija/Login/", saljemo)
      .subscribe((x:LoginInformacije) =>{
        if (x.isLogiran) {
          porukaSuccess("uspjesan login");
          AutentifikacijaHelper.setLoginInfo(x)
          this.router.navigateByUrl("/pocetna");
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null)
          porukaError("neispravan login");
        }
      });
  }
}
