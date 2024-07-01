import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';
import { IChat } from '../models/chat.model';
import { AuthService } from './auth.service';
import { IUser } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class ChatSignalRService {
  private hubConnection!: signalR.HubConnection;
  private messageSubject = new BehaviorSubject<IChat | null>(null);
  public message$ = this.messageSubject.asObservable();
  private unreadMessages: { [receiverId: string]: IChat[] } = {};
  private unreadCountSubject = new BehaviorSubject<number>(0);
  public unreadCount$ = this.unreadCountSubject.asObservable();
  private usersSubject = new BehaviorSubject<IUser[]>([]);
  public users$ = this.usersSubject.asObservable();

  constructor(private authService: AuthService) {
    this.loadMessagesFromStorage();
    this.startConnection().then(() => {
      this.addListeners();
    });
  }

  private async startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/chatHub`)
      .build();

    try {
      await this.hubConnection.start();
      console.log('SignalR connection established.');
    } catch (err) {
      console.error('Error while starting SignalR connection: ' + err);
    }
  }

  private addListeners() {
    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      const chatMessage: IChat = {
        id: '',
        sender: user,
        senderId: '',
        receiver: '',
        receiverId: '',
        message: message,
        read: false,
        timeStamp: new Date()
      };
      this.messageSubject.next(chatMessage);
      this.trackUnreadMessage(chatMessage);
    });

    this.hubConnection.on('ReceiveUsersByMessages', (users: IUser[]) => {
      this.usersSubject.next(users);
    });
  }

  private trackUnreadMessage(message: IChat) {
    if (message.receiverId !== localStorage.getItem('userId')) {
      console.log('Message received for current user. Tracking unread.');

      if (!this.unreadMessages[message.receiverId]) {
        this.unreadMessages[message.receiverId] = [];
      }

      this.unreadMessages[message.receiverId].push(message);
      this.saveMessagesToStorage();
      this.updateUnreadCount();
    }
  }

  private updateUnreadCount() {
    let count = 0;
    Object.keys(this.unreadMessages).forEach(receiverId => {
      count += this.unreadMessages[receiverId].length;
    });
    this.unreadCountSubject.next(count);
  }

  public getUnreadCount() {
    return this.unreadCountSubject.asObservable();
  }

  public markMessagesAsRead(receiverId: string) {
    if (this.unreadMessages[receiverId]) {
      delete this.unreadMessages[receiverId];
      this.saveMessagesToStorage();
      this.updateUnreadCount();
    }
  }

  private saveMessagesToStorage() {
    localStorage.setItem('unreadMessages', JSON.stringify(this.unreadMessages));
  }

  private loadMessagesFromStorage() {
    const storedMessages = localStorage.getItem('unreadMessages');
    console.log('unread: ', storedMessages);
    if (storedMessages) {
      this.unreadMessages = JSON.parse(storedMessages);
      this.updateUnreadCount();
    }
  }

  public async sendMessageToAdmin(message: string) {
    const adminId = 'ac100f97-6db1-42ba-b3ad-a0881b167e50';
    const senderName = this.authService.getName();
    const chatMessage: IChat = {
      id: '',
      sender: senderName || '',
      senderId: localStorage.getItem('userId') || '',
      receiver: 'Admin',
      receiverId: adminId,
      message: message,
      read: false,
      timeStamp: new Date()
    };
    this.messageSubject.next(chatMessage);
    this.trackUnreadMessage(chatMessage);

    try {
      await this.hubConnection.invoke('SendMessageToAdmin', 'User', message);
    } catch (err) {
      console.error('Error while sending message: ' + err);
    }
  }

  public async sendMessageToUser(userId: string, message: string) {
    const chatMessage: IChat = {
      id: '',
      sender: 'Admin',
      senderId: 'ac100f97-6db1-42ba-b3ad-a0881b167e50',
      receiver: 'User',
      receiverId: userId,
      message: message,
      read: false,
      timeStamp: new Date()
    };
    this.messageSubject.next(chatMessage);
    this.trackUnreadMessage(chatMessage);

    try {
      await this.hubConnection.invoke('SendMessageToUser', 'Admin', userId, message);
    } catch (err) {
      console.error('Error while sending message: ' + err);
    }
  }

  public async getUsersByMessages() {
    try {
      await this.hubConnection.invoke('GetUsersByMessages');
    } catch (err) {
      console.error('Error while retrieving users by messages: ' + err);
    }
  }
}
