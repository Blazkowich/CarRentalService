import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { IVehicle } from './../../models/vehicle.model';
import { CommonModule } from '@angular/common';
import { VehicleDetailsService } from '../../services/vehicle-details.service';
import { Subscription } from 'rxjs';

@Component({
  templateUrl: './page-details.component.html',
  styleUrls: ['./page-details.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class VehicleDetailComponent implements OnInit, OnDestroy {
  pageTitle: string = "Vehicle Detail";
  errorMessage = '';
  vehicle: IVehicle | undefined;
  sub!: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleDetailsService: VehicleDetailsService) { }

    ngOnInit(): void {
      const id = this.route.snapshot.paramMap.get('id');
      if (id) {
        this.sub = this.vehicleDetailsService.getVehicle(id).subscribe({
          next: vehicle => {
            this.vehicle = vehicle;
          },
          error: err => this.errorMessage = err
        });
      } else {
        this.errorMessage = 'Vehicle ID not found';
      }
    }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  getVehicle(id: string): void {
    this.vehicleDetailsService.getVehicle(id).subscribe({
      next: vehicle => this.vehicle = vehicle,
      error: err => this.errorMessage = err
    });
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
