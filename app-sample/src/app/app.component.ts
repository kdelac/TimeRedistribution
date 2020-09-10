import { Component } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { Guid } from "guid-typescript";
import { ApplicationService } from './services/application.service';
import { Application } from './models/Application';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

application: Application[];

constructor(
  public signalRService: SignalRService,
  private _workDayService: ApplicationService) { }

ngOnInit(): void {
  this.signalRService.startConnection();
  this.signalRService.dataListenerNumberOfPeople();
  this.signalRService.dataListenerMaxNumberReached();

}

login(){
  const id = Guid.create();
  const gu = "35C2909D-0A96-43F5-9ABE-07F6766E6A34"
  this.signalRService.sendLogin(id.toString(), gu.toString());
}

logout(){
  this._workDayService.getApplications().subscribe(_ =>
    {
      this.application = _ as Application[]

      let rand = Math.floor((Math.random() * this.application.length) + 1);

      if(this.application.length == 1)
        rand = 0;

      console.log(this.application[rand].patientId, this.application[rand].ordinationId);
      this.signalRService
      .sendLogout(this.application[rand].patientId, this.application[rand].ordinationId);
    })

}

}
