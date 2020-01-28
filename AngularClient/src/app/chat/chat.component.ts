import { UserService } from '../shared/user.service';
import { Component, OnInit, NgZone, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Message } from '../models/message';
import { ChatService } from '../shared/chat.service';
import { Subject } from 'rxjs';
import { takeUntil, map } from 'rxjs/operators';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
 
})
export class ChatComponent implements OnInit, OnDestroy {
  title = 'ChatClient';
  txtMessage: string = '';
  uniqueID: string = new Date().getTime().toString();
  messages = new Array<Message>();
  message = {} as Message;
  history: Message[]=[];
  private destroyed$: Subject<void> = new Subject();

  constructor(private router: Router, private service: UserService,private chatService: ChatService,private ngZone: NgZone) { 
    this.subscribeToEvents();
  }

  ngOnInit() {
    this.chatService.getHistoryOfMessages().pipe(takeUntil(this.destroyed$),map(val => <Message[]>val)).subscribe(res => {
      this.history = res;
      },
      err => {
        console.log(err);
      });
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  sendMessage(): void {
    if (this.txtMessage) {
      this.message = {} as Message;
      this.message.sender = localStorage.getItem('userName');
      this.message.message = this.txtMessage;
      this.message.date = new Date();
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';
    }
  }
  private subscribeToEvents(): void {

    this.chatService.messageReceived.subscribe((message: Message) => {
      this.ngZone.run(() => {    
          this.messages.push(message);  
      });
    });
  }
  
  onLogout() {
    localStorage.removeItem('userName');
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  
}