import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-rezervacija',
  templateUrl: './rezervacija.component.html',
  styleUrls: ['./rezervacija.component.css']
})
export class RezervacijaComponent {

  rezervacijaId: number;
  register:FormGroup;
  tipoviKartice=['Kreditna', 'Debitna'];

  constructor(private httpKlijent: HttpClient, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(s => {
      this.rezervacijaId = +s['id'];
      this.register=this.formBuilder.group({
        brojOsoba: new FormControl('', [
          Validators.required,
          Validators.pattern('[0-9]'),]),
        djeca: new FormControl('', [
          Validators.required,
          Validators.pattern('[0-9]'),]),
        checkIn: new FormControl('', [
          Validators.required,
        ]),
        checkOut: new FormControl('', [
          Validators.required,
        ]),
      });
    })
  }

  get brojOsoba() : FormControl{
    return this.register.get("brojOsoba") as FormControl;
  }
  get brojDjece() : FormControl{
    return this.register.get("brojDjece") as FormControl;
  }
  get checkIn() : FormControl{
    return this.register.get("checkIn") as FormControl;
  }
  get checkOut() : FormControl{
    return this.register.get("checkOut") as FormControl;
  }
  get brojKartice() : FormControl{
    return this.register.get("brojKartice") as FormControl;
  }
  get tipKartice() : FormControl{
    return this.register.get("tipKartice") as FormControl;
  }

  collect() {

  }
}
