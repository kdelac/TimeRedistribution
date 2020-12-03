
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RepositoryService } from './Repository.service';
import { Application } from '../models/Application';
import { Message } from '../models/message';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  constructor() { }

  messages: Message[] =  [ {text:'Prvi Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},
  {text:'Trx Summary', senderName:'Kristijan'},

  ];

  getMessages () {
        return of(this.messages)
  }

  add (){
    this.messages.push({senderName: 'Kristijan', text:'Novaporuka'})
  }
}
