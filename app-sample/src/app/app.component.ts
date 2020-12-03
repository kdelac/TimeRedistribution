import { Component, ElementRef, ViewChild } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { Guid } from "guid-typescript";
import { ApplicationService } from './services/application.service';
import { Application } from './models/Application';
import { Message } from './models/message';
import { Observable } from 'rxjs';
import { MessageService } from './services/message.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('scroll') private myScrollContainer: ElementRef;


  messages : Observable<Message[]> ;
  element: HTMLDivElement;
options: ScrollToOptions;
displayedColumns: string[] = ['senderName', 'text'];

constructor(private _messageService:MessageService) { }

ngOnInit(): void {
this.getProductsUsingAsyncPipe();
}

ngAfterViewInit(): void {
  //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
  //Add 'implements AfterViewInit' to the class.

}

ngAfterViewChecked(): void {
  //Called after every check of the component's view. Applies to components only.
  //Add 'implements AfterViewChecked' to the class.
  this.scrollToBottom();
}

add(){
  this._messageService.add();
  this.getProductsUsingAsyncPipe();
}

public getProductsUsingAsyncPipe() {
  this.messages = this._messageService.getMessages();
}

scrollToBottom(): void {
  try {
      this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
  } catch(err) {
    console.log(err)
  }
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
