import { Component, OnDestroy, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { IVehicle } from '../models/vehicle.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule]
})
export class MainPageComponent implements OnInit, OnDestroy {
  title = 'CarRental Rental Service';
  vehicles: IVehicle[] = [];
  availableVehicles: IVehicle[] = [];
  filteredVehicles: IVehicle[] = [];
  imageWidth = 200;
  imageMargin = 0;
  errorMessage: string = '';
  sub!: Subscription;
  showImage = true;

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }

  set listFilter(value: string) {
    this._listFilter = value.trim().toLocaleLowerCase();
    console.log('In setter', this._listFilter);
    this.filteredVehicles = this.performFilter(this._listFilter);
  }

  constructor(private vehicleService: VehicleService, private authService: AuthService) { }

  ngOnInit(): void {
    this.sub = this.vehicleService.getVehicles().subscribe({
      next: vehicles => {
        this.vehicles = vehicles;
        this.filteredVehicles = this.vehicles;
      },
      error: err => this.errorMessage = err
    });
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  toggleImage(): void {
    this.showImage = !this.showImage;
  }

  performFilter(filterBy: string): IVehicle[] {
    if (!filterBy) return this.vehicles;

    filterBy = filterBy.toLocaleLowerCase();
    return this.vehicles.filter((vehicle: IVehicle) =>
      vehicle.Name.toLocaleLowerCase().includes(filterBy)
    );
  }
}
