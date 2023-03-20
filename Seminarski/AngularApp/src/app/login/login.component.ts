import {Component, OnInit} from '@angular/core';
import {FormControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../helper/login-informacije";
import {MojConfig} from "../../MojConfig";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LogInComponent implements OnInit{
  LogInForma:FormGroup;
  submitted=false;
  success=false;
  korisnickoIme:any;
  lozinka:any;



  constructor(private formBuilder:FormBuilder, private router : Router, private httpKlijent:HttpClient) {

    this.LogInForma=this.formBuilder.group({
      korisnickoIme:new FormControl('', Validators.required),
      lozinka:new FormControl('', Validators.required)
    })
  }

  onSubmit()
  {
    this.submitted=true;

    if(this.LogInForma.invalid)
    {
      return;
    }

    let saljemo = {
      korisnickoIme:this.korisnickoIme,
      lozinka: this.lozinka
    };
    this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera + "/Autentifikacija/Login/", saljemo,MojConfig.http_opcije())
      .subscribe((x:LoginInformacije) =>{
        if (x.isLogiran) {
          porukaSuccess("Uspješna prijava!");
          AutentifikacijaHelper.setLoginInfo(x)
          this.router.navigate(['nekretnine/', 0]);
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null)
          porukaError("Prijava nije uspješna!");
        }
      });
  }

  ngOnInit (){
    this.korisnickoIme="amina.muhibic";
    this.lozinka="Amina123";
  }

}
