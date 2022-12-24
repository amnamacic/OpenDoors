import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../MojConfig";
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  constructor(private autentifikacijahelper: AutentifikacijaHelper, private router: Router,private httpKlijent:HttpClient) {
  }

  appTitle: string = 'OpenDoors';

  loginInfo():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  logOut() {
    AutentifikacijaHelper.setLoginInfo(null);

    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/LogOut/", null, MojConfig.http_opcije())
      .subscribe((x: any) => {
        this.router.navigateByUrl("/login");
        //porukaSuccess("Logout uspješan.");
      });
  }
}
