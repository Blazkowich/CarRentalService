import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChatService } from '../../services/chat.service';
import { FormsModule } from '@angular/forms';
import { IChat } from '../../models/chat.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-support-chat',
  templateUrl: './support-chat.component.html',
  styleUrls: ['./support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class ChatComponent {
  title = 'Chat With Support';
  message: string = '';
  messages: IChat[] = [];
  chatSub!: Subscription;

  constructor(private chatService: ChatService, private router: Router) {}

  sendMessage(): void {
    if (this.message) {
      this.chatSub = this.chatService.sendMessage(this.message).subscribe(
        () => {
          console.log('Message sent successfully');
          this.loadMessages();
        },
        (error) => {
          console.error('Send message error:', error);
        }
      );
    } else {
      console.error('Message is required.');
    }
  }

  loadMessages(): void {
    const userId = 'currentUserId'; // Replace with actual user ID retrieval logic
    this.chatSub = this.chatService.getChatMessages(userId).subscribe(
      (messages) => {
        this.messages = messages;
      },
      (error) => {
        console.error('Load messages error:', error);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.chatSub) {
      this.chatSub.unsubscribe();
    }
  }

  onBack(): void {
    this.router.navigate(['/user-page']);
  }
}
