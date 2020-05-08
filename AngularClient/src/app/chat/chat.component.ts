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
  txtMessage: string;
  messages:Message[];
  message: Message;
  history: Message[];
  private destroyed$: Subject<void>;

  constructor(private router: Router, private userService: UserService,private chatService: ChatService,private ngZone: NgZone) {}

  ngOnInit() {
    this.txtMessage = '';
    this.message = new Message();
    this.history = [];
    this.messages = [];
    this.destroyed$ = new Subject();
    
    this.subscribeToEvents();
    this.chatService.getHistoryOfMessages().pipe(takeUntil(this.destroyed$),map(val => <Message[]>val))
    .subscribe(res => {
      this.history = res;
      },
      error => {
        console.log(error);
      });
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
    this.chatService.stopSignalR();
  }

  private subscribeToEvents(): void {
    this.chatService.messageReceived
    .subscribe((message: Message) => {
      this.ngZone.run(() => {    
          this.messages.push(message);  
      });
    });
  }

  public sendMessage(): void {
    if (this.txtMessage) {
      this.message.sender = localStorage.getItem('userName');
      this.message.message = this.txtMessage;
      this.message.date = new Date();
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';
    }
  }
  
  public onLogout(): void {
  this.userService.logout();
  } 

}