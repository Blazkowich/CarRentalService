import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IVehicle } from '../../../models/vehicle.model';
import { VehicleService } from '../../../services/vehicle.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

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

  vehicleTypes = [
    'Sedan',
    'Coupe',
    'Van',
    'HatchBack',
    'SUV',
    'Jeep',
    'MiniVan',
    'Limousine'
  ];
  reservationTypes = ['Reserved', 'Free'];

  constructor(private vehicleService: VehicleService, private router: Router) {}

  ngOnInit(): void {}

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
}
