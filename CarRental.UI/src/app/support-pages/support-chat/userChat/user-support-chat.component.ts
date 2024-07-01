import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChatSignalRService } from '../../../services/signalR.service';
import { FormsModule } from '@angular/forms';
import { IChat } from '../../../models/chat.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AdminPageService } from '../../../services/admin-page.service';

@Component({
  selector: 'app-support-chat',
  templateUrl: './user-support-chat.component.html',
  styleUrls: ['./user-support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule]
})
export class UserChatComponent implements OnInit, AfterViewChecked {
  title = 'Chat With Support';
  message: string = '';
  messages: IChat[] = [];

  reversedMessages: IChat[] = [];

  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  constructor(
    private chatService: ChatSignalRService,
    private adminService: AdminPageService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
  }

  loadChat(): void {
  }

  sendMessage(): void {
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
      this.messageContainer.nativeElement.scrollTop = this.messageContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
