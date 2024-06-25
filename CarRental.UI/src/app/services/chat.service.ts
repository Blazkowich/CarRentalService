import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { IChat } from "../models/chat.model";
import { ErrorHandleService } from "../shared/error.handle";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = 'https://localhost:7060/chat';

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  sendMessage(message: string): Observable<void> {
    const url = `${this.apiUrl}/send`;
    return this.http.post<void>(url, { message }).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }

  getChatMessages(userId: string): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages/${userId}`;
    return this.http.get<IChat[]>(url).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }

  getAllChatMessages(): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages`;
    return this.http.get<IChat[]>(url).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }
}
