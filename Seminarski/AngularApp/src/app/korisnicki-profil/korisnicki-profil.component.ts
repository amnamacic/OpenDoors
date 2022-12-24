import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";


@Component({
  selector: 'app-korisnicki-profil',
  templateUrl: './korisnicki-profil.component.html',
  styleUrls: ['./korisnicki-profil.component.css']
})
export class KorisnickiProfilComponent  implements OnInit{

  constructor(private httpKlijent: HttpClient, private router :Router) {
  }
  novaSifra:any;
  promjenaLozinke=false;

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.novaSifra={id:this.loginInfo().autentifikacijaToken.korisnickiNalog.id,
      staraLozinka:"", novaLozinka:""};
  }

  PromijeniPassword() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/KrajnjiKorisnik/PromjeniLozinku`, this.novaSifra, MojConfig.http_opcije()).subscribe(x=>{
      this.promjenaLozinke=false;
      AutentifikacijaHelper.setLoginInfo(x=null);
      this.router.navigateByUrl("/login");
    });
  }
}
