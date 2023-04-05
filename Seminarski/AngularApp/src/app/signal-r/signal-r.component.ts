import {Component, Injectable} from '@angular/core';
import * as signalR from  '@microsoft/signalr';
import {MojConfig} from "../../MojConfig";

@Injectable({
  providedIn:"root"
})

@Component({
  selector: 'app-signal-r',
  templateUrl: './signal-r.component.html',
  styleUrls: ['./signal-r.component.css']
})
export class SignalRComponent {
  public brojKomentara:number;

  zapocniKonekciju(nekId:number) {

    var connection = new signalR.HubConnectionBuilder()
      .withUrl(MojConfig.adresa_servera+ '/recenzijeHub')
      .build();

    connection.on('CountKomentara', (p:number)=>{
      this.brojKomentara = p;
    });

    connection.start().then(()=>{
        console.log("otvoren kanal WS");
      }
    ).then(()=>{
      connection?.invoke("ProslijediPoruku", nekId)
       ;
    });
  }
}
