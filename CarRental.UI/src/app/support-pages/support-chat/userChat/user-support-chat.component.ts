import { AfterViewChecked, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChatService } from '../../../services/chat.service';
import { FormsModule } from '@angular/forms';
import { IChat } from '../../../models/chat.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-support-chat',
  templateUrl: './user-support-chat.component.html',
  styleUrls: ['./user-support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule]
})
export class UserChatComponent implements OnInit, OnDestroy, AfterViewChecked {
  title = 'Chat With Support';
  message: string = '';
  messages: IChat[] = [];
  reversedMessages: IChat[] = [];
  chatSub: Subscription[] = [];
  userId: string | null = null;
  notificationCount = 0;
  lastMessageCount = 0;

  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  constructor(
    private chatService: ChatService,
    private router: Router,
  ) {
    this.userId = localStorage.getItem('userId');
  }

  ngOnInit(): void {
    this.loadMessages();
    this.chatService.newMessage().subscribe((messages: IChat[]) => {
      this.messages = messages;
      if (this.userId) {
        this.scrollToBottom();
      } else {
        console.warn('User ID is not set when new messages arrive.');
      }
    });
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.messageContainer.nativeElement.scrollTop = this.messageContainer.nativeElement.scrollHeight;
    } catch(err) { }
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
    if (this.userId) {
      this.chatService.getChatMessages(this.userId).subscribe(
        (messages: IChat[]) => {
          this.reversedMessages = messages.slice().reverse();
          this.messages = messages;
          messages.forEach(message => {
            if (!message.read && message.receiverId == this.userId) {
              this.markMessageAsRead(message.id);
            }
          });
          this.scrollToBottom();
        },
        (error) => {
          console.error('Load messages error:', error);
        }
      );
    } else {
      console.error('User ID not found.');
    }
  }

  markMessageAsRead(messageId: string): void {
    this.chatService.markMessageAsRead(messageId).subscribe(
      () => {
        console.log();
      },
      (error) => {
        console.error(`Error marking message ${messageId} as read:`, error);
      }
    );
  }

  ngOnDestroy(): void {
    this.chatSub.forEach(sub => sub.unsubscribe());
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
