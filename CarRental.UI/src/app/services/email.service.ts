import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError } from "rxjs";
import { IEmail } from "../models/email.model";
import { ErrorHandleService } from "../shared/error.handle";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private apiUrl = `${environment.apiUrl}/email`;

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  sendEmail(subject: string, message: string): Observable<IEmail> {
    const url = `${this.apiUrl}/send`;
    const requestBody = {
      Subject: subject,
      Message: message
    };

    return this.http.post<IEmail>(url, requestBody).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }
}
