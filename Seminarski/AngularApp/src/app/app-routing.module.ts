import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { PomocComponent } from './pomoc/pomoc.component';
import { KategorijaComponent } from './kategorija/kategorija.component';
import {CommonModule} from "@angular/common";
import {LoginComponent} from "./login/login.component";
import {NekretnineComponent} from "./nekretnine/nekretnine.component";
import {KuceComponent} from "./kuce/kuce.component";
import {StanoviComponent} from "./stanovi/stanovi.component";
import {RegistracijaComponent} from "./registracija/registracija.component";


const routes: Routes = [
  { path: 'pocetna', component: PocetnaComponent},
  { path: 'pomoc', component: PomocComponent},
  { path: 'kategorija', component: KategorijaComponent },
  { path: 'login', component: LoginComponent },
  { path: 'nekretnine', component: NekretnineComponent },
  { path: 'kuce', component: KuceComponent },
  { path: 'stanovi', component: StanoviComponent },
  { path: 'registracija', component: RegistracijaComponent }
];

@NgModule({
  imports: [CommonModule,
    RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
