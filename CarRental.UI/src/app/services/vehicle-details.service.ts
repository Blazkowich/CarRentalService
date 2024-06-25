import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IVehicle } from '../models/vehicle.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleDetailsService {
  private getVehicleByIdapiUrl = 'https://localhost:7060/vehicles/byId';
  private getAvailabilityDateapiUrl = 'https://localhost:7060/booking/availableFrom';

  constructor(private http: HttpClient) { }

  getVehicle(id: string): Observable<IVehicle | undefined> {
    const url = `${this.getVehicleByIdapiUrl}/${id}`;
    return this.http.get<IVehicle>(url).pipe(
      catchError(this.handleError)
    );
  }

  getAvailableDate(id: string): Observable<string> {
    const url = `${this.getAvailabilityDateapiUrl}/${id}`;
    return this.http.get<string>(url, { responseType: 'text' as 'json' }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }

    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
