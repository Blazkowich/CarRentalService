import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { VehicleService } from '../../services/vehicle.service';
import { AuthService } from '../../services/auth.service';
import { IVehicle } from '../../models/vehicle.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css'],
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule]
})

export class UserPageComponent implements OnInit {
  title = 'Cabinet';
  availableVehicles: IVehicle[] = [];

  constructor(private vehicleService: VehicleService, private authService: AuthService) {}

  ngOnInit(): void {
    this.getAvailableVehicles();
  }

  getAvailableVehicles(): void {
    this.vehicleService.getAvailableVehicles().subscribe(
      (vehicles: IVehicle[]) => {
        this.availableVehicles = vehicles;
      },
      (error: any) => {
        console.error('Error fetching available vehicles:', error);
      }
    );
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
