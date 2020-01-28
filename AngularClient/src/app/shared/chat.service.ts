import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from '../models/message';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connectionEstablished = new EventEmitter<Boolean>();
  readonly BaseURI = 'http://localhost:5000/api';
  
  private connectionIsEstablished = false;
  private _hubConnection: HubConnection;

  constructor(private http: HttpClient) {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendMessage(message: Message) {
    this._hubConnection.invoke('NewMessage', message);
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/SignalR/PublicChat')
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionEstablished.emit(true);
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(function () { this.startConnection(); }, 5000);
      });
  }
  getHistoryOfMessages() {
    return this.http.get(this.BaseURI + '/PublicChat/MessagesHistory');
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('MessageReceived', (data: any) => {
      this.messageReceived.emit(data);
    });
  }
}  