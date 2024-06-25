import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { IVehicle } from '../models/vehicle.model';
import { ErrorHandleService } from '../shared/error.handle';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = 'https://localhost:7060/vehicles';

  constructor(private http: HttpClient, private errorHanlde: ErrorHandleService) { }

  getVehicles(): Observable<IVehicle[]> {
    return this.http.get<IVehicle[]>(this.apiUrl).pipe(
      catchError(this.errorHanlde.handleError));
  }

  getAvailableVehicles(): Observable<IVehicle[]> {
    return this.http.get<IVehicle[]>(`${this.apiUrl}/available`).pipe(
      catchError(this.errorHanlde.handleError));
  }

  getVehicleById(id: string): Observable<IVehicle | undefined> {
    return this.http.get<IVehicle>(`${this.apiUrl}/byId/${id}`).pipe(
      catchError(this.errorHanlde.handleError));
  }
  getVehicle(id: string): Observable<IVehicle | undefined> {
    return this.getVehicles()
      .pipe(
        map((vehicles: IVehicle[]) => vehicles.find(p => p.Id === id))
      );
  }
}
