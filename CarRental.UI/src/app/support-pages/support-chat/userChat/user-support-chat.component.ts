import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChatService } from '../../../services/chat.service';
import { FormsModule } from '@angular/forms';
import { IChat } from '../../../models/chat.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-support-chat',
  templateUrl: './user-support-chat.component.html',
  styleUrls: ['./user-support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule]
})
export class UserChatComponent implements OnInit, OnDestroy {
  title = 'Chat With Support';
  message: string = '';
  messages: IChat[] = [];
  chatSub: Subscription[] = [];
  userId: string | null = null;

  constructor(
    private chatService: ChatService,
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    const chatsData = this.route.snapshot.paramMap.get('chats');
    if (chatsData) {
      this.messages = JSON.parse(chatsData);
    }
    this.loadMessages();
    this.chatService.newMessage().subscribe((messages: IChat[]) => {
      this.messages = messages;
    });
  }

  sendMessage(): void {
    if (this.message) {
      const sendSub = this.chatService.sendMessageToSupport(this.message).subscribe(
        () => {
          console.log('Message sent successfully');
          this.message = '';
          this.loadMessages();
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
      this.chatService.getChatMessages(this.userId).subscribe(
        (messages: IChat[]) => {
          this.messages = messages;
        },
        (error) => {
          console.error('Load messages error:', error);
        }
      );
    } else {
      console.error('User ID not found.');
    }
  }

  ngOnDestroy(): void {
    this.chatSub.forEach(sub => sub.unsubscribe());
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
