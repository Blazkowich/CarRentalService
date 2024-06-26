import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'authToken';
  private userKey = 'firstName';
  private IdKey = 'userId';
  private logoutTimeout: any;

  constructor(private router: Router) { }

  setTokenAndName(
    token: string,
    firstName: string,
    userId: string
    ): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userKey, firstName);
    localStorage.setItem(this.IdKey, userId);
    this.startLogoutTimer();
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getName(): string | null {
    return localStorage.getItem(this.userKey);
  }

  getUserId(): string | null {
    return localStorage.getItem(this.IdKey);
  }

  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  logOut(): void {
    this.clearToken();
    this.clearUserName();
    this.clearUserId();
    this.router.navigate(['/login']);
  }

  getUserName(): string | null {
    return localStorage.getItem(this.userKey);
  }

  setUserName(userName: string): void {
    localStorage.setItem(this.userKey, userName);
  }

  clearUserName(): void {
    localStorage.removeItem(this.userKey);
  }

  clearUserId(): void {
    localStorage.removeItem(this.IdKey);
  }

  startLogoutTimer(): void {
    this.clearLogoutTimer();
    this.logoutTimeout = setTimeout(() => {
      this.logOut();
    }, 10 * 60 * 1000); // 10 minute
  }

  clearLogoutTimer(): void {
    if (this.logoutTimeout) {
      clearTimeout(this.logoutTimeout);
    }
  }

  resetLogoutTimer(): void {
    this.startLogoutTimer();
  }
}
