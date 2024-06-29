import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/vehicle.model';
import { VehicleService } from '../../services/vehicle.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule, formatDate } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class UserPageComponent implements OnInit {
  title = 'Cabinet';
  availableVehicles: IVehicle[] = [];
  vehicles: IVehicle[] = []

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
  ) {}

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
}
