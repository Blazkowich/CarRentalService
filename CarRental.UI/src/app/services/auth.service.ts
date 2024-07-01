import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'authToken';
  private userKey = 'firstName';
  private IdKey = 'userId';
  private roleKey = 'role';
  private logoutTimeout: any;

  constructor(private router: Router) { }

  setTokenAndName(
    token: string,
    firstName: string,
    userId: string,
    role: string
    ): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userKey, firstName);
    localStorage.setItem(this.IdKey, userId);
    localStorage.setItem(this.roleKey, role);
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

  getRole(): string | null {
    return localStorage.getItem(this.roleKey);
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
    this.clearRole();
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

  clearRole(): void {
    localStorage.removeItem(this.roleKey);
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

  isAdmin(): boolean {
    const userRole = this.getCurrentUserRole();
    return userRole && userRole === "Admin";
  }

  isUser(): boolean {
    const userRole = this.getCurrentUserRole();
    return userRole && userRole === "User";
  }

  getCurrentUserRole(): any {
    const userRole = localStorage.getItem(this.roleKey);
    return userRole;
  }
}
