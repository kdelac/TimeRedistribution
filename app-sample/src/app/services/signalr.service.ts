import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { timeStamp } from 'console';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public inside: Number;
  public outside: Number;
  public patientId: Number;
  public ordinationId: Number;


private hubConnection: signalR.HubConnection

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:5001/nophub')
                            .build();

    this.hubConnection
      .start()
      .then(() => console.log(this.hubConnection))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public dataListenerNumberOfPeople = () => {
    this.hubConnection.on('NumberOfPeople', (inside, outside) => {
      this.inside = inside;
      this.outside = outside;
    });
  }

  public dataListenerMaxNumberReached = () => {
    this.hubConnection.on('MaxNumberReached', (patientId, ordinationId) => {
      this.patientId = patientId;
      this.ordinationId = ordinationId;
      console.log('Max number reached!!!!!!!!!')
    });
  }

  public sendLogin = (numberW, numberO) => {
    this.hubConnection.send('SendMessageLogin',numberW, numberO)
  }

  public sendLogout = (numberW, numberO) => {
    this.hubConnection.send('SendMessageLogout', numberW, numberO)
  }

}
