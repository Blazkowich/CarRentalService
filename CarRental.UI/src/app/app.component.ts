import { CommonModule } from '@angular/common';
import { Component, ElementRef, HostListener, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';
import { IChat } from './models/chat.model';
import { ChatService } from './services/chat.service';
import { Subscription, interval } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [
    RouterOutlet,
    CommonModule,
    RouterModule,
  ]
})
export class AppComponent implements OnInit, OnDestroy{
  title = 'CarRental Rental Service';
  userName: string | null = null;
  userId: string | null = null;
  showUserOptions = false;
  isLightMode = true;
  notificationCount = 0;
  @ViewChild('userOptionsContainer') userOptionsContainer!: ElementRef;
  private intervalSubscription: Subscription | undefined;

  constructor(private authService: AuthService, private chatService: ChatService) {
    this.userId = localStorage.getItem('userId')
    console.log(this.userId);
    if (this.isLoggedIn()) {
      this.startNotificationInterval();
    }
    console.log(this.notificationCount);
  }

  ngOnInit(): void {
    this.authService.startLogoutTimer();
    this.addClickOutsideListener();
    this.subscribeToNewMessages();
  }

  ngOnDestroy(): void {
    this.stopNotificationInterval();
  }

  updateNotificationCount(): void {
    if (this.isLoggedIn()) {
      this.chatService.getNotificationCount(this.userId!).subscribe(
        (count: number) => {
          this.notificationCount = count;
          console.log('Notification Count:', this.notificationCount);
        },
        (error) => {
          console.error('Error fetching notification count:', error);
        }
      );
    }
  }

  addClickOutsideListener(): void {
    document.addEventListener('click', this.onClickOutside.bind(this));
  }

  onClickOutside(event: MouseEvent): void {
    if (this.showUserOptions &&
      !this.userOptionsContainer.nativeElement.contains(event.target)) {
      this.showUserOptions = false;
    }
  }

  @HostListener('window:mousemove')
  @HostListener('window:keypress')
  @HostListener('window:click')
  @HostListener('window:scroll')
  @HostListener('window:touchstart')
  @HostListener('window:touchmove')
  @HostListener('window:keydown')
  onUserActivity() {
    this.authService.resetLogoutTimer();
  }

  startNotificationInterval(): void {
    if (!this.intervalSubscription) {
      this.intervalSubscription = interval(5000).subscribe(() => {
        this.updateNotificationCount();
      });
    }
  }

  stopNotificationInterval(): void {
    if (this.intervalSubscription) {
      this.intervalSubscription.unsubscribe();
      this.intervalSubscription = undefined;
    }
  }

  fetchName(): string | null {
    return this.authService.getName();
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  logout(): void {
    this.authService.logOut();
    this.stopNotificationInterval();

  }

  toggleUserOptions(): void {
    this.showUserOptions = !this.showUserOptions;
  }

  private subscribeToNewMessages(): void {
    this.chatService.newMessage().subscribe((messages: IChat[]) => {
      const unreadMessages = messages.filter(msg => !msg.Read && msg.SenderId !== this.userId);
      this.notificationCount = unreadMessages.length;
    });
  }
}
