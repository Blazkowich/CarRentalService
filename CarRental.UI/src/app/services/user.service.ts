import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { IUser } from '../models/user.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7060/auth';

  constructor(private http: HttpClient, private authService: AuthService) { }

  logIn(login: string, password: string): Observable<IUser> {
    const url = `${this.apiUrl}/login`;
    const requestBody = {
      Model: {
        Login: login,
        Password: password,
      },
    };

    return this.http.post<IUser>(url, requestBody).pipe(
      catchError(this.handleError),
      tap((response) => {
        if (response && response.Token) {
          this.authService.setToken(response.Token);
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
      catchError(this.handleError)
    );
  }

  logout(): void {
    this.authService.logOut();
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.status}`;
    }
    else {
      errorMessage = `Server Returned code: ${err.status}, error message is: ${err.message}`
    }

    console.log(errorMessage);
    return throwError(() => errorMessage);
  }
}
