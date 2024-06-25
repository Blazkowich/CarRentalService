import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IBooking } from "../models/booking.model";
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";
import { ErrorHandleService } from "../shared/error.handle";

@Injectable({
  providedIn: 'root'
})
export class UserPageService {
  private vehiclesApiUrl = 'https://localhost:7060/booking';

  constructor(private http: HttpClient, private errorHandler: ErrorHandleService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getBookingHistory(userId: string): Observable<IBooking[]> {
    const url = `${this.vehiclesApiUrl}/bookingHistoryByUser/${userId}`;
    return this.http.get<IBooking[]>(url, { headers: this.getAuthHeaders() }).pipe(
      catchError(this.errorHandler.handleError)
    );
  }

  getActiveBooking(userId: string): Observable<IBooking[]> {
    const url = `${this.vehiclesApiUrl}/activeBookingByUser/${userId}`;
    return this.http.get<IBooking[]>(url, { headers: this.getAuthHeaders() }).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
