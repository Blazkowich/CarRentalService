import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { IBooking } from "../models/booking.model";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = `${environment.apiUrl}/booking`;

  constructor(private http: HttpClient) {}

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
}
