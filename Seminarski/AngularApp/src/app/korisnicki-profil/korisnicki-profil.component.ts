import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {MojConfig} from "../../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {NavComponent} from "../nav/nav.component";

declare function porukaSuccess(a: string): any;

declare function porukaError(a: string): any;

@Component({
  selector: 'app-korisnicki-profil',
  templateUrl: './korisnicki-profil.component.html',
  styleUrls: ['./korisnicki-profil.component.css'],
  providers: [NavComponent]
})
export class KorisnickiProfilComponent  implements OnInit{
  rezervacije: any;

  constructor(private httpKlijent: HttpClient, private router :Router, private odjava:NavComponent) {
  }
  novaSifra:any;
  promjenaLozinke=false;
  promjenaSlike: boolean;

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.novaSifra={id:this.loginInfo().autentifikacijaToken.korisnickiNalog.id,
      staraLozinka:"", novaLozinka:""};
  }

  PromijeniPassword() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/PromijeniLozinku`, this.novaSifra, MojConfig.http_opcije()).subscribe(x=>{
      this.promjenaLozinke=false;
      AutentifikacijaHelper.setLoginInfo(x=null);
      this.router.navigateByUrl("/login");
    });
  }

  otvoriNekretnineVlasnika(s:any){
    this.router.navigate(['nekretnine-vlasnik/', s]);
  }

  otvoriRezervacijeKorisnika(id: number) {
    this.httpKlijent.get(MojConfig.adresa_servera+ "/Rezervacija/GetByKorisnikId?korisnikId=" + id,MojConfig.http_opcije()).subscribe(x=>{
      this.rezervacije = x;
    });
  }

  otkaziRezervaciju(id:number) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Rezervacija/OtkaziRezervaciju/${id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.otvoriRezervacijeKorisnika(this.loginInfo().autentifikacijaToken.korisnickiNalogId);
    });
  }

  getSlikuKorisnika(korisnickiNalogId: number) {
    return `${MojConfig.adresa_servera}/Korisnik/GetSlikuKorisnika/${korisnickiNalogId}`;
  }

  novaSlika:any;
  generisiPreview() {
    // @ts-ignore
    var file = document.getElementById("formFile").files[0];
    if (file) {
      var reader = new FileReader();
      let this2=this;
      reader.onload=function ()
      {
        this2.novaSlika.slikaKorisnika=reader.result.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  promijeniSliku() {
    this.novaSlika={id:this.loginInfo().autentifikacijaToken.korisnickiNalogId, slikaKorisnika:""};
    this.promjenaSlike=true;
  }

  spasiNovuSliku() {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/PromijeniSliku`, this.novaSlika, MojConfig.http_opcije()).subscribe(x=>{
      this.promjenaSlike=false;
      porukaSuccess("Uspješno ste promijenili profilnu sliku!");
    });
  }

}
