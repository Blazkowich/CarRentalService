import { IVehicle } from './../models/vehicle.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ErrorHandleService } from '../shared/error.handle';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleDetailsService {
  private apiUrl = `${environment.apiUrl}`;

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  getVehicle(id: string): Observable<IVehicle | undefined> {
    const url = `${this.apiUrl}/vehicles/byId/${id}`;
    return this.http.get<IVehicle>(url).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }

  getAvailableDate(id: string): Observable<string> {
    const url = `${this.apiUrl}/booking/availableFrom/${id}`;
    return this.http.get<string>(url, { responseType: 'text' as 'json' }).pipe(
      catchError(this.errorHanlde.handleError)
    );
  }
}
