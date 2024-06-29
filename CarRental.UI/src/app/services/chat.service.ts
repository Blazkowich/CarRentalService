import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subscription, catchError, concatMap, interval, map } from "rxjs";
import { IChat } from "../models/chat.model";
import { ErrorHandleService } from "../shared/error.handle";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = `${environment.apiUrl}/chat`;
  private newMessageSubject = new BehaviorSubject<IChat[]>([]);
  private pollingSubscription: Subscription | undefined;
  userId: string | null = null;

  constructor(
    private http: HttpClient, private errorHandle: ErrorHandleService) {
      this.userId = localStorage.getItem('userId');
    if (this.userId) {
      this.startPolling(this.userId);
    }
     }

  private startPolling(userId: string): void {
    this.pollingSubscription = interval(5000)
      .pipe(
        concatMap(() => this.getChatMessages(userId))
      )
      .subscribe({
        next: (messages: IChat[]) => {
          this.newMessageSubject.next(messages);
        },
        error: () => { }
      });
    }

  private stopPolling(): void {
    if (this.pollingSubscription) {
      this.pollingSubscription.unsubscribe();
    }
  }

  setUserLoggedIn(userId: string): void {
    this.userId = userId;
    this.startPolling(userId);
  }

  setUserLoggedOut(): void {
    this.userId = null;
    this.stopPolling();
  }

  getNotificationCount(userId: string): Observable<number> {
    const url = `${this.apiUrl}/unread/count/${userId}`;
    return this.http.get<number>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        return this.errorHandle.handleError(error);
      })
    );
  }

  sendMessageToSupport(message: string): Observable<void> {
    const url = `${this.apiUrl}/send`;
    return this.http.post<void>(url, { message }).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(`Error sending message: ${error.message}`, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  sendMessageToUser(userId: string, message: string): Observable<void> {
    const url = `${this.apiUrl}/sendtouser`;
    return this.http.post<void>(url, { userId, message }).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(`Error sending message: ${error.message}`, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  getChatMessages(userId: string): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages/${userId}`;
    return this.http.get<IChat[]>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error();
        return this.errorHandle.handleError(error);
      })
    );
  }

  getChatMessagesForAdmin(userId: string): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages/foradmin/${userId}`;
    return this.http.get<IChat[]>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(`Error fetching chat messages for user ${userId}: ${error.message}`, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  getAllChatMessages(): Observable<IChat[]> {
    const url = `${this.apiUrl}/messages`;
    return this.http.get<IChat[]>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('Error fetching all chat messages:', error.message, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  markMessageAsRead(messageId: string): Observable<void> {
    const url = `${this.apiUrl}/messages/${messageId}/read`;
    return this.http.patch<void>(url, {}).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(`Error marking message as read: ${error.message}`, error);
        return this.errorHandle.handleError(error);
      })
    );
  }

  notifyNewMessage(messages: IChat[]): void {
    this.newMessageSubject.next(messages);
  }

  newMessage(): Observable<IChat[]> {
    return this.newMessageSubject.asObservable();
  }
}
