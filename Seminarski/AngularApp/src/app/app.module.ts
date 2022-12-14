import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http'
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AppComponent} from "./app.component";
import { NavComponent } from './nav/nav.component';
import {RouterLink, RouterModule, RouterOutlet} from "@angular/router";
import { PomocComponent } from './pomoc/pomoc.component';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { KategorijaComponent } from './kategorija/kategorija.component';
import {RouterTestingModule} from "@angular/router/testing";
import {AppRoutingModule} from "./app-routing.module";
import { LoginComponent } from './login/login.component';
import { NekretnineComponent } from './nekretnine/nekretnine.component';
import { KuceComponent } from './kuce/kuce.component';
import { StanoviComponent } from './stanovi/stanovi.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PomocComponent,
    PocetnaComponent,
    KategorijaComponent,
    LoginComponent,
    NekretnineComponent,
    KuceComponent,
    StanoviComponent,

  ],
  exports: [RouterModule],
  bootstrap:[AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterOutlet,
    RouterLink,
    RouterTestingModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [],
})
export class AppModule { }

export class MojConfig{
  static adresa_servera = "https://localhost:7115";
}
