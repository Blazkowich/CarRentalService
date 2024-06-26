import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { IChat } from "../models/chat.model";
import { ErrorHandleService } from "../shared/error.handle";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'https://localhost:7060/chat';

  constructor(
    private http: HttpClient, private errorHandle: ErrorHandleService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    if (!token) {
      throw new Error('No auth token found');
    }
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  sendMessage(message: string): Observable<void> {
    const url = `${this.apiUrl}/send`;
    const headers = this.getAuthHeaders();
    return this.http.post<void>(url, { message }, { headers }).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(`Error sending message: ${error.message}`, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  getChatMessages(userId: string): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages/${userId}`;
    const headers = this.getAuthHeaders();
    return this.http.get<IChat[]>(url, { headers }).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(`Error fetching chat messages for user ${userId}: ${error.message}`, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  getAllChatMessages(): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages`;
    const headers = this.getAuthHeaders();
    return this.http.get<IChat[]>(url, { headers }).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error fetching all chat messages:', error.message, error);
        return this.errorHandle.handleError(error);
      })
    );
  }
}
