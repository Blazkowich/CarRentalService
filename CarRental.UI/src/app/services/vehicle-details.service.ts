import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IVehicle } from '../models/vehicle.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleDetailsService {
  private apiUrl = 'https://localhost:7060/vehicles/byId';

  constructor(private http: HttpClient) { }

  getVehicle(id: string): Observable<IVehicle | undefined> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<IVehicle>(url).pipe(
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
