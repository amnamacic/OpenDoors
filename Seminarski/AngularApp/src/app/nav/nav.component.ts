import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {Router} from "@angular/router";
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  constructor(private autentifikacijahelper: AutentifikacijaHelper, private router: Router) {
  }

  appTitle: string = 'OpenDoors';

  loginInfo():LoginInformacije{
    return AutentifikacijaHelper.getLoginInfo();
  }

  logOut() {
    this.autentifikacijahelper.removeToken();
    this.router.navigateByUrl("/login");
  }
}
