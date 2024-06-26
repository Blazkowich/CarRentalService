import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';

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
export class AppComponent implements OnInit{
  title = 'CarRental Rental Service';
  userName: string | null = null;
  showUserOptions = false;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.startLogoutTimer();
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

  fetchName(): string | null {
    return this.authService.getName();
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout(): void {
    this.authService.logOut();
  }

  toggleUserOptions(): void {
    this.showUserOptions = !this.showUserOptions;
  }
}
