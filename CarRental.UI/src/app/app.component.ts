import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
export class AppComponent {
  title = 'CarRental Rental Service';
  userName: string | null = null;

  constructor(private authService: AuthService) { }

  fetchName(): string | null {
    return this.authService.getName();
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout(): void {
    this.authService.logOut();
  }
}
