import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ErrorHandleService } from "../shared/error.handle";
import { Observable, catchError, tap } from "rxjs";
import { IUser } from "../models/user.model";
import { IChat } from "../models/chat.model";
import { environment } from "../../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class AdminPageService {
private chatApiUrl = `${environment.apiUrl}/chat`;

  constructor(private http: HttpClient, private errorHandler: ErrorHandleService) { }

  getChatByUserId(userId: string): Observable<IChat[]> {
    const url = `${this.chatApiUrl}/messages/${userId}`;
    return this.http.get<IChat[]>(url).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  getUsersByMessages(): Observable<IUser[]> {
    const url = `${this.chatApiUrl}/getUsersByMessages`;
    return this.http.get<IUser[]>(url).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
