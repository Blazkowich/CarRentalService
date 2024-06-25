import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { IVehicle } from '../models/vehicle.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = 'https://localhost:7060/vehicles';

  constructor(private http: HttpClient) { }

  getVehicles(): Observable<IVehicle[]> {
    return this.http.get<IVehicle[]>(this.apiUrl).pipe(
      catchError(this.handleError));
  }

  getVehicle(id: string): Observable<IVehicle | undefined> {
    return this.getVehicles()
      .pipe(
        map((vehicles: IVehicle[]) => vehicles.find(p => p.Id === id))
      );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.status}`;
    }
    else {
      errorMessage = `Server Returned code: ${err.status}, error message is: ${err.message}`
    }

    console.log(errorMessage);
    return throwError(() => errorMessage);
  }
}
