import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IChat } from '../models/chat.model';

@Injectable({
  providedIn: 'root'
})
export class ChatDataService {
  private chatSubject = new BehaviorSubject<IChat[]>([]);
  chats$ = this.chatSubject.asObservable();

  setChats(chats: IChat[]): void {
    this.chatSubject.next(chats);
  }
}
