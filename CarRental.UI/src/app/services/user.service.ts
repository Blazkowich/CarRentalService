import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { IUser } from '../models/user.model';
import { AuthService } from './auth.service';
import { ErrorHandleService } from '../shared/error.handle';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7060/auth';

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
          if (response && response.Token) {
            this.authService.setTokenAndName(response.Token, response.FirstName, response.Id);
            console.log(response.FirstName, response.Id);
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

  logout(): void {
    this.authService.logOut();
  }
}
