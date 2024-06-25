import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";


@Injectable({
  providedIn: 'root'
})
export class UserPageService {
  private vehiclesApi = 'https://localhost:7060/vehicles';

  constructor(private http: HttpClient) { }
}
