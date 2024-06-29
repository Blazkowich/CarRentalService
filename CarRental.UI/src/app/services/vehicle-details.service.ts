import { IVehicle } from './../models/vehicle.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ErrorHandleService } from '../shared/error.handle';
import { environment } from '../../environments/environment';
import { mapVehicleApiToApp } from '../shared/mappers/vehicle.mapper';

@Injectable({
  providedIn: 'root'
})
export class VehicleDetailsService {
  private getVehicleByIdapiUrl = `${environment.apiUrl}/vehicles/byId`;
  private getAvailabilityDateapiUrl = `${environment.apiUrl}/booking/availableFrom`;

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  getVehicle(id: string): Observable<IVehicle | undefined> {
    const url = `${this.getVehicleByIdapiUrl}/${id}`;
    return this.http.get<IVehicle>(url).pipe(
      map(mapVehicleApiToApp),
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
