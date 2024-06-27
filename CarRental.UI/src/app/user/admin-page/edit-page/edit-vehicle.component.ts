import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IVehicle } from '../../../models/vehicle.model';
import { VehicleService } from '../../../services/vehicle.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-edit-vehicle-page',
  templateUrl: './edit-vehicle.component.html',
  styleUrls: ['./edit-vehicle.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule]
})
export class EditVehiclePageComponent implements OnInit {
  title = 'Edit Vehicle';
  vehicle: IVehicle | undefined;

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

  constructor(
    private vehicleService: VehicleService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.vehicleService.getVehicleById(id).subscribe(
        (vehicle: IVehicle | undefined) => {
          this.vehicle = vehicle;
          console.log(this.vehicle);
        },
        (error: any) => {
          console.error('Error fetching vehicle:', error);
        }
      );
    }
  }

  onSubmit(): void {
    if (this.vehicle) {
      this.vehicleService.updateVehicle(this.vehicle).subscribe(
        (updatedVehicle: IVehicle) => {
          console.log('Vehicle updated successfully', updatedVehicle);
          this.router.navigate(['/admin-page']);
        },
        (error: any) => {
          console.error('Error updating vehicle:', error);
        }
      );
    }
  }

  onCancel(): void {
    this.router.navigate(['/admin-page']);
  }
}
