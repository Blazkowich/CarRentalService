import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, catchError, map } from "rxjs";
import { IBooking } from "../models/booking.model";
import { environment } from "../../environments/environment";
import { mapBookingApiToApp } from "../shared/mappers/booking.mapper";
import { ErrorHandleService } from "../shared/error.handle";

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = `${environment.apiUrl}/booking`;

  constructor(private http: HttpClient, private errorHandler: ErrorHandleService) {}

  reserveVehicle(vehicleId: string, startDate: Date, duration: number): Observable<IBooking> {
    const body = { vehicleId, startDate, duration };
    return this.http.post<IBooking>(`${this.apiUrl}/reservation`, body );
  }

  bookVehicle(vehicleId: string, duration: number): Observable<IBooking> {
    const body = { vehicleId, duration };
    return this.http.post<IBooking>(`${this.apiUrl}/book`, body);
  }

  cancelBooking(bookingRequest: IBooking): Observable<IBooking> {
    return this.http.post<IBooking>(`${this.apiUrl}/cancel`, bookingRequest);
  }

  getBookingHistory(userId: string): Observable<IBooking[]> {
    const url = `${this.apiUrl}/bookingHistoryByUser/${userId}`;
    return this.http.get<IBooking[]>(url).pipe(
      map((response: IBooking[]) => response.map(mapBookingApiToApp)),
      catchError(this.errorHandler.handleError)
    );
  }

  getActiveBooking(userId: string): Observable<IBooking[]> {
    const url = `${this.apiUrl}/activeBookingByUser/${userId}`;
    return this.http.get<IBooking[]>(url).pipe(
      map((response: IBooking[]) => response.map(mapBookingApiToApp)),
      catchError(this.errorHandler.handleError)
    );
  }
}
