import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { MessageComponent } from './Components/message/message.component';
import { MessagesComponent } from './Components/messages/messages.component';
import {MatGridListModule} from '@angular/material/grid-list';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { ScrollingDirective } from './Directives/scrolling.directive';

@NgModule({
  declarations: [
    AppComponent,
    MessageComponent,
    MessagesComponent,
    ScrollingDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatGridListModule,
    ScrollingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
