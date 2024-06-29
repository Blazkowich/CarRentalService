import { Injectable } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { Observable, catchError, map, tap } from 'rxjs';
import { IVehicle } from '../models/vehicle.model';
import { ErrorHandleService } from '../shared/error.handle';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = `${environment.apiUrl}/vehicles`;

  constructor(private http: HttpClient, private errorHandle: ErrorHandleService) { }

  getVehicles(): Observable<IVehicle[]> {
    return this.http.get<any[]>(this.apiUrl).pipe(
      catchError(this.errorHandle.handleError)
    );
  }

  getAvailableVehicles(): Observable<IVehicle[]> {
    return this.http.get<IVehicle[]>(`${this.apiUrl}/available`).pipe(
      catchError(this.errorHandle.handleError));
  }

  getVehicleById(id: string): Observable<IVehicle> {
    return this.http.get<any>(`${this.apiUrl}/byId/${id}`).pipe(
      catchError(this.errorHandle.handleError)
    );
  }

  addVehicle(vehicle: IVehicle): Observable<IVehicle> {
    return this.http.post<IVehicle>(this.apiUrl, vehicle ).pipe(
      catchError(this.errorHandle.handleError));
  }

  updateVehicle(vehicle: IVehicle): Observable<IVehicle> {
    return this.http.put<IVehicle>(this.apiUrl, vehicle ).pipe(
      catchError(this.errorHandle.handleError));
  }

  deleteVehicle(id: string): Observable<{}> {
    return this.http.delete(`${this.apiUrl}`, { body: { Id: id } }).pipe(
      catchError(this.errorHandle.handleError));
  }

  getVehicleTypes(): Observable<string[]> {
    const url = `${this.apiUrl}/getVehicleTypes`;
    return this.http.get<string[]>(url).pipe(
      catchError(this.errorHandle.handleError)
    );
  }
}
