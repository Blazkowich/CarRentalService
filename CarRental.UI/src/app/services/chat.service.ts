import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { IChat } from "../models/chat.model";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'https://localhost:7060/chat';

  constructor(private http: HttpClient) { }

  sendMessage(message: string): Observable<void> {
    const url = `${this.apiUrl}/send`;
    return this.http.post<void>(url, { message }).pipe(
      catchError(this.handleError)
    );
  }

  getChatMessages(userId: string): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages/${userId}`;
    return this.http.get<IChat[]>(url).pipe(
      catchError(this.handleError)
    );
  }

  getAllChatMessages(): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages`;
    return this.http.get<IChat[]>(url).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.status}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }

    console.log(errorMessage);
    return throwError(() => errorMessage);
  }
}
