import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChatService } from '../../services/chat.service';
import { FormsModule } from '@angular/forms';
import { IChat } from '../../models/chat.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-support-chat',
  templateUrl: './support-chat.component.html',
  styleUrls: ['./support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule]
})
export class ChatComponent implements OnInit {
  title = 'Chat With Support';
  message: string = '';
  messages: IChat[] = [];
  chatSub: Subscription[] = [];
  userId: string | null = null;

  constructor(
    private chatService: ChatService,
    private router: Router,
    private authService: AuthService) {}

    ngOnInit(): void {
      this.loadMessages();
    }

    sendMessage(): void {
      if (this.message) {
        const sendSub = this.chatService.sendMessage(this.message).subscribe(
          () => {
            console.log('Message sent successfully');
            this.loadMessages();
            this.message = '';
          },
          (error) => {
            console.error('Send message error:', error);
          }
        );
        this.chatSub.push(sendSub);
      } else {
        console.error('Message is required.');
      }
    }

    loadMessages(): void {
      this.userId = this.authService.getUserId();
      if (this.userId) {
        this.messages = [];
        const loadSub = this.chatService.getChatMessages(this.userId).subscribe(
          (messages) => {
            this.messages = messages;
            console.log(this.messages);
          },
          (error) => {
            console.error('Load messages error:', error);
          }
        );
        this.chatSub.push(loadSub);
      } else {
        console.error('User ID not found.');
      }
    }

  ngOnDestroy(): void {
    this.chatSub.forEach(sub => sub.unsubscribe());
  }

  onBack(): void {
    this.router.navigate(['/user-page']);
  }
}
