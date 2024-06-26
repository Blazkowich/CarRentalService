import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChatService } from '../../../services/chat.service';
import { FormsModule } from '@angular/forms';
import { IChat } from '../../../models/chat.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-admin-support-chat',
  templateUrl: './admin-support-chat.component.html',
  styleUrls: ['./admin-support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule]
})
export class AdminChatComponent implements OnInit, OnDestroy {
  title = 'Chat With User';
  message: string = '';
  messages: IChat[] = [];
  chatSub: Subscription[] = [];
  userId: string | null = null;
  userName: string | null = null;

  constructor(
    private chatService: ChatService,
    private router: Router,
    private authService: AuthService,
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    const encodedChatsData = this.route.snapshot.paramMap.get('chats');

    if (encodedChatsData) {
      try {
        const decodedChatsData = decodeURIComponent(encodedChatsData);
        const parsedChats: string[] = JSON.parse(decodedChatsData);
        const extractedName = parsedChats.map(chat => chat.split(' - ')[0])[0];
        this.userName = extractedName || null;
      } catch (e) {
        console.error('Failed to parse chat data', e);
      }
    }

    if (this.userName) {
      this.getUserIdFromUserName();
    } else {
      console.error('User name is missing in chat data.');
    }

    this.chatService.newMessage().subscribe((messages: IChat[]) => {
      if (this.userId) {
        this.loadMessages();
      } else {
        console.warn('User ID is not set when new messages arrive.');
      }
    });
  }

  getUserIdFromUserName(): void {
    this.userService.getUserIdByName(this.userName!).subscribe(
      (userId: string) => {
        this.userId = userId;
        this.loadMessages(); // Load messages after obtaining userId
      },
      (error: any) => {
        console.error('Error fetching userId:', error);
      }
    );
  }

  loadMessages(): void {
    if (this.userId) {
      this.chatService.getChatMessagesForAdmin(this.userId).subscribe(
        (messages: IChat[]) => {
          this.messages = messages; // Update messages on successful fetch
        },
        (error: any) => {
          console.error('Error loading messages:', error);
        }
      );
    } else {
      console.error('User ID is not set.');
    }
  }

  sendMessage(): void {
    if (this.message && this.userId) {
      const sendSub = this.chatService.sendMessageToUser(this.userId, this.message).subscribe(
        () => {
          console.log('Message sent successfully');
          this.message = '';
          this.loadMessages(); // Optionally reload messages after sending
        },
        (error) => {
          console.error('Send message error:', error);
        }
      );
      this.chatSub.push(sendSub);
    } else {
      console.error('Message is required or User ID is missing.');
    }
  }

  ngOnDestroy(): void {
    this.chatSub.forEach(sub => sub.unsubscribe());
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
