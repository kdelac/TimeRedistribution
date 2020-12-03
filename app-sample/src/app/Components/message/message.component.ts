import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MessageComponent {

  @Input() message : Message;
}
