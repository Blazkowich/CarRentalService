import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, tap } from 'rxjs';
import { IUser } from '../models/user.model';
import { AuthService } from './auth.service';
import { ErrorHandleService } from '../shared/error.handle';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/auth`;
  private userApiUrl = `${environment.apiUrl}/users`;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private errorHanlde: ErrorHandleService) { }

    logIn(login: string, password: string): Observable<IUser> {
      const url = `${this.apiUrl}/login`;
      const requestBody = {
        Model: {
          Login: login,
          Password: password,
        },
      };

      return this.http.post<IUser>(url, requestBody).pipe(
        catchError(this.errorHanlde.handleError),
        tap((response) => {
          if (response && response.token) {
            this.authService.setTokenAndName(response.token, response.firstName, response.id, response.role);
          }
        })
      );
    }

  register(
    UserName: string,
    FirstName: string,
    LastName: string,
    Email: string,
    PhoneNumber: number,
    Address: string,
    Password: string
  ): Observable<IUser> {
    const url = `${this.apiUrl}/register`;
    const requestBody = {
      UserName,
      FirstName,
      LastName,
      Email,
      PhoneNumber,
      Address,
      Password
    };
    return this.http.post<IUser>(url, requestBody).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }

  getUserIdByName(userName: string): Observable<string> {
    const url = `${this.userApiUrl}/byName/${userName}`;
    return this.http.get<string>(url);
  }

  logout(): void {
    this.authService.logOut();
  }
}
