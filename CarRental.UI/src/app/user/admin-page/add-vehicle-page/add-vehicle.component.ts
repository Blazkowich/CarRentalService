import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IVehicle } from '../../../models/vehicle.model';
import { VehicleService } from '../../../services/vehicle.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BookingService } from '../../../services/booking.service';

@Component({
  selector: 'app-add-vehicle-page',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule]
})

export class AddVehiclePageComponent implements OnInit {
  title = 'Add Vehicle';
  newVehicle: IVehicle = {
    id: '',
    name: '',
    description: '',
    type: '',
    reservationType: '',
    price: 0,
    imageUrl: ''
  };

  vehicleTypes: string[] = [];
  reservationTypes: string[] = [];

  constructor(
    private vehicleService: VehicleService,
    private bookingService: BookingService,
    private router: Router) {}

  ngOnInit(): void {
    this.getVehicleTypes();
    this.getReservationTypes();
  }

  onSubmit(): void {
    this.vehicleService.addVehicle(this.newVehicle).subscribe(
      (vehicle: IVehicle) => {
        console.log('Vehicle added successfully', vehicle);
        this.router.navigate(['/admin-page']);
      },
      (error: any) => {
        console.error('Error adding vehicle:', error);
      }
    );
  }

  onCancel(): void {
    this.router.navigate(['/admin-page']);
  }

  getVehicleTypes(): void {
    this.vehicleService.getVehicleTypes().subscribe(
      (types: string[]) => {
        this.vehicleTypes = types;
      },
      (error: any) => {
        console.error('Error fetching vehicle types:', error);
      }
    );
  }

  getReservationTypes(): void {
    this.bookingService.getReservationTypes().subscribe(
      (types: string[]) => {
        this.reservationTypes = types;
      },
      (error: any) => {
        console.error('Error fetching vehicle types:', error);
      }
    );
  }
}
