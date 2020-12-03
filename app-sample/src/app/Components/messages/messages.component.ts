import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MessagesComponent {

  @Input() messages;
}
