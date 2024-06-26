import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { ErrorHandleService } from "../shared/error.handle";
import { Observable, catchError, tap } from "rxjs";
import { IUser } from "../models/user.model";


@Injectable({
  providedIn: 'root'
})
export class AdminPageService {
private chatApiUrl = 'https://localhost:7060/chat';

  constructor(private http: HttpClient, private errorHandler: ErrorHandleService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getUsersByMessages(): Observable<IUser[]> {
    const url = `${this.chatApiUrl}/getUsersByMessages`;
    return this.http.get<IUser[]>(url, { headers: this.getAuthHeaders() }).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
