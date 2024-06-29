import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, throwError } from "rxjs";
import { IEmail } from "../models/email.model";
import { ErrorHandleService } from "../shared/error.handle";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private apiUrl = `${environment.apiUrl}/email`;

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  sendEmail(subject: string, message: string): Observable<IEmail> {
    const url = `${this.apiUrl}/send`;
    const requestBody = {
      Subject: subject,
      Message: message
    };

    return this.http.post<IEmail>(url, requestBody, { headers: this.getAuthHeaders() }).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }
}
