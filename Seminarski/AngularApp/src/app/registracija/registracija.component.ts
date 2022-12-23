import {Component, OnInit} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {HttpClient} from "@angular/common/http";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";


declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit{
  krajnjiKorisniciPodaci: any;
  vlasnikPodaci: any;
  repeatPass:string='none';
  register:FormGroup;


  gradPodaci: any;


  constructor(private httpKlijent: HttpClient, private router: Router, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void{
    this.fetchVlasnici();
    this.fetchKrajnjiKorisnici();
    this.register=this.formBuilder.group({
      ime: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.pattern('[a-zA-Z]*'),]),
      prezime: new FormControl('', [
        Validators.required,
        Validators.minLength(2),
        Validators.pattern('[a-zA-Z]*'),]),
      spol: new FormControl('',[
        Validators.required,
      ]),
      godinaRodjenja: new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]{4}'),
        Validators.min(1955),
        Validators.max(2004),
      ]),
      brojTelefona: new FormControl('',[
        Validators.required,
        Validators.pattern("[0-9]{3}/[0-9]{3}-[0-9]{3,4}")
      ]),
      username: new FormControl('', [
        Validators.required,
        Validators.pattern("[a-zA-Z].*"),
        Validators.minLength(6),
        Validators.maxLength(12)
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(15)
      ]),

      email: new FormControl('', [
        Validators.required,
        Validators.email
      ]),
    });
  }

  collect() {
    if(this.register.valid){
      this.httpKlijent.post(`${MojConfig.adresa_servera}/KrajnjiKorisnik/Snimi`, this.register.value,MojConfig.http_opcije()).subscribe(x=>{
        console.warn("rezultat",x);
        //this.router.navigateByUrl("/pocetna");
      });

      let saljemo = {
        korisnickoIme:this.username.value,
        lozinka: this.password.value
      };
      this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera+ "/Autentifikacija/Login/", saljemo)
        .subscribe((x:LoginInformacije) =>{
          if (x.isLogiran) {

            AutentifikacijaHelper.setLoginInfo(x)
            this.router.navigateByUrl("/pocetna");
          }
          else
          {
            AutentifikacijaHelper.setLoginInfo(null)

          }
        });
    }
    else
      console.table(this.register.value)
  }

  private fetchKrajnjiKorisnici() : void{
    this.httpKlijent.get(MojConfig.adresa_servera+ "/KrajnjiKorisnik/GetAll",MojConfig.http_opcije()).subscribe((x:any)=>{
      this.krajnjiKorisniciPodaci = x;
    })
  }

  private fetchVlasnici():void {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Vlasnik/GetAll",MojConfig.http_opcije()).subscribe((x:any)=>{
      this.vlasnikPodaci = x;
    })
  }

  get ime() : FormControl{
    return this.register.get("ime") as FormControl;
  }
  get prezime() : FormControl{
    return this.register.get("prezime") as FormControl;
  }
  get spol() : FormControl{
    return this.register.get("spol") as FormControl;
  }
  get godinaRodjenja() : FormControl{
    return this.register.get("godinaRodjenja") as FormControl;
  }
  get brojTelefona() : FormControl{
    return this.register.get("brojTelefona") as FormControl;
  }
  get username() : FormControl{
    return this.register.get("username") as FormControl;
  }
  get password() : FormControl{
    return this.register.get("password") as FormControl;
  }

  get email() : FormControl{
    return this.register.get("email") as FormControl;
  }
}

