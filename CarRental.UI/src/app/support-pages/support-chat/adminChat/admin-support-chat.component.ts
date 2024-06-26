import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { Observable, Subscription, catchError, tap, throwError } from 'rxjs';
import { ChatService } from '../../../services/chat.service';
import { IChat } from '../../../models/chat.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

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
  userIdFromChat: string | null = null;
  userName: string | null = null;
  notificationCount = 0;
  lastMessageCount = 0;

  @ViewChild('messageContainer') private messageContainer!: ElementRef;


  constructor(
    private chatService: ChatService,
    private router: Router,
    private userService: UserService
  ) {
    this.userId = localStorage.getItem('userId')
  }

  ngOnInit(): void {
    this.loadMessages().subscribe(
      (messages: IChat[]) => {
        this.messages = messages;

        this.chatService.newMessage().subscribe((newMessages: IChat[]) => {
          this.messages = newMessages;
          this.scrollToBottom();
        });

        if (!this.userId && this.userName) {
          this.getUserIdFromUserName();
        }
      },
      (error) => {
        console.error('Failed to load messages:', error);
      }
    );
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.messageContainer.nativeElement.scrollTop = this.messageContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }

  getUserIdFromUserName(): void {
    this.userService.getUserIdByName(this.userName!).subscribe(
      (userId: string) => {
        this.userId = userId;
        this.loadMessages().subscribe();
      },
      (error: any) => {
        console.error('Error fetching userId:', error);
      }
    );
  }

  loadMessages(): Observable<IChat[]> {
    if (!this.userId) {
      console.error('User ID not found.');
      return new Observable<IChat[]>(observer => {
        observer.error('User ID not found.');
      });
    }

    return this.chatService.getChatMessagesForAdmin(this.userId).pipe(
      tap((messages: IChat[]) => {
        this.messages = messages;
        messages.forEach(message => {
          if (message.Sender !== "Admin") {
            this.userIdFromChat = message.SenderId;
          } else {
            this.userIdFromChat = message.ReceiverId;
          }
          if (!message.Read && message.ReceiverId === this.userId) {
            this.markMessageAsRead(message.Id);
          }
        });
        this.scrollToBottom();
      }),
      catchError((error) => {
        console.error('Load messages error:', error);
        return throwError(error);
      })
    );
  }

  markMessageAsRead(messageId: string): void {
    this.chatService.markMessageAsRead(messageId).subscribe(
      () => {
        console.log(`Message ${messageId} marked as read.`);
      },
      (error) => {
        console.error(`Error marking message ${messageId} as read:`, error);
      }
    );
  }

  ngOnDestroy(): void {
    this.chatSub.forEach(sub => sub.unsubscribe());
  }

  sendMessage(): void {
    if (this.message && this.userIdFromChat) {
      const sendSub = this.chatService.sendMessageToUser(this.userIdFromChat, this.message).subscribe(
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
      console.error('Message is required or User ID is missing.');
    }
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
