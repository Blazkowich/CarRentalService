import { IChat } from './../../../models/chat.model';
import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ChatSignalRService } from '../../../services/signalR.service';
import { AdminPageService } from '../../../services/admin-page.service';

@Component({
  selector: 'app-admin-support-chat',
  templateUrl: './admin-support-chat.component.html',
  styleUrls: ['./admin-support-chat.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule]
})
export class AdminChatComponent implements OnInit, AfterViewChecked {
  title = 'Chat With User';
  message: string = '';
  messages: IChat[] = [];

  reversedMessages: IChat[] = [];

  @ViewChild('messageContainer') private messageContainer!: ElementRef;

  constructor(
    private chatService: ChatSignalRService,
    private route: ActivatedRoute,
    private adminService: AdminPageService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
  }

  scrollToBottom(): void {
    try {
      this.messageContainer.nativeElement.scrollTop = this.messageContainer.nativeElement.scrollHeight;
    } catch(err) { }
  }

  loadChat(): void {
  }

  sendMessage(): void {
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
