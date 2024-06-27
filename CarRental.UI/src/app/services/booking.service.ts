import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { IBooking } from "../models/booking.model";

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = 'https://localhost:7060/booking';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    if (!token) {
      console.log();
    }
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  reserveVehicle(vehicleId: string, startDate: Date, duration: number): Observable<IBooking> {
    const body = { vehicleId, startDate, duration };
    const headers = this.getAuthHeaders();
    return this.http.post<IBooking>(`${this.apiUrl}/reservation`, body, {headers});
  }

  bookVehicle(vehicleId: string, duration: number): Observable<IBooking> {
    const headers = this.getAuthHeaders();
    const body = { vehicleId, duration };
    return this.http.post<IBooking>(`${this.apiUrl}/book`, body, {headers});
  }

  cancelBooking(bookingRequest: IBooking): Observable<IBooking> {
    const headers = this.getAuthHeaders();
    return this.http.post<IBooking>(`${this.apiUrl}/cancel`, bookingRequest, {headers});
  }
}
