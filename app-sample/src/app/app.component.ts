import { Component, ElementRef, ViewChild } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { Guid } from "guid-typescript";
import { ApplicationService } from './services/application.service';
import { Application } from './models/Application';
import { Message } from './models/message';
import { Observable } from 'rxjs';
import { MessageService } from './services/message.service';
import { ScrollingDirective } from './Directives/scrolling.directive';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild(ScrollingDirective) scroll: ScrollingDirective

  messages : Observable<Message[]> ;
  element: HTMLDivElement;
  options: ScrollToOptions;
  displayedColumns: string[] = ['senderName', 'text'];
  first:boolean = false;
  position: string = 'bottom';
  newMessages:boolean = false;

constructor(private _messageService:MessageService) { }

ngOnInit(): void {
this.getProductsUsingAsyncPipe();
}

add(){
  this.newMessages = !this.newMessages;
  this._messageService.add();
  this.getProductsUsingAsyncPipe();
}

public getProductsUsingAsyncPipe() {
  this.messages = this._messageService.getMessages();
}



// login(){
//   const id = Guid.create();
//   const gu = "35C2909D-0A96-43F5-9ABE-07F6766E6A34"
//   this.signalRService.sendLogin(id.toString(), gu.toString());
// }

// logout(){
//   this._workDayService.getApplications().subscribe(_ =>
//     {
//       this.application = _ as Application[]

//       let rand = Math.floor((Math.random() * this.application.length) + 1);

//       if(this.application.length == 1)
//         rand = 0;

//       console.log(this.application[rand].patientId, this.application[rand].ordinationId);
//       this.signalRService
//       .sendLogout(this.application[rand].patientId, this.application[rand].ordinationId);
//     })

// }

}
