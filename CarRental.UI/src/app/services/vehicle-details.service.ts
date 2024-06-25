import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IVehicle } from '../models/vehicle.model';
import { ErrorHandleService } from '../shared/error.handle';

@Injectable({
  providedIn: 'root'
})
export class VehicleDetailsService {
  private getVehicleByIdapiUrl = 'https://localhost:7060/vehicles/byId';
  private getAvailabilityDateapiUrl = 'https://localhost:7060/booking/availableFrom';

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  getVehicle(id: string): Observable<IVehicle | undefined> {
    const url = `${this.getVehicleByIdapiUrl}/${id}`;
    return this.http.get<IVehicle>(url).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }

  getAvailableDate(id: string): Observable<string> {
    const url = `${this.getAvailabilityDateapiUrl}/${id}`;
    return this.http.get<string>(url, { responseType: 'text' as 'json' }).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }
}
