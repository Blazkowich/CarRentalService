import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = '';
  private userKey = '';

  constructor() { }

  setTokenAndName(token: string, firstName: string): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userKey, firstName);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getName(): string | null {
    return localStorage.getItem(this.userKey);
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
}
