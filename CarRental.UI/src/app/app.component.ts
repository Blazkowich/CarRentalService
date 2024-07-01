import { Component, ElementRef, HostListener, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from './services/auth.service';
import { ChatSignalRService } from './services/signalR.service';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    RouterModule,
  ]
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'CarRental Rental Service';
  userName: string | null = null;
  userId: string | null = null;
  showUserOptions = false;
  isLightMode = true;
  notificationCount = 0;
  @ViewChild('userOptionsContainer') userOptionsContainer!: ElementRef;
  private unreadCountSubscription: Subscription | undefined;

  constructor(private authService: AuthService, private chatSignalRService: ChatSignalRService) {
    this.userId = localStorage.getItem('userId');
    if (this.isLoggedIn()) {
      this.subscribeToUnreadCount();
    }
  }

  ngOnInit(): void {
    this.authService.startLogoutTimer();
    this.addClickOutsideListener();
  }

  ngOnDestroy(): void {
    if (this.unreadCountSubscription) {
      this.unreadCountSubscription.unsubscribe();
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

  subscribeToUnreadCount(): void {
    this.unreadCountSubscription = this.chatSignalRService.getUnreadCount().subscribe(count => {
      if (count > this.notificationCount) {
        this.notificationCount = count;
      }
    });
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
    if (this.unreadCountSubscription) {
      this.unreadCountSubscription.unsubscribe();
    }
    this.notificationCount = 0; // Reset notification count on logout
  }

  toggleUserOptions(): void {
    this.showUserOptions = !this.showUserOptions;
  }
}
