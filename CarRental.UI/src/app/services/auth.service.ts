import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'authToken';
  private userKey = 'firstName';
  private IdKey = 'userId';

  constructor() { }

  setTokenAndName(
    token: string,
    firstName: string,
    userId: string
    ): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userKey, firstName);
    localStorage.setItem(this.IdKey, userId);
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
}
