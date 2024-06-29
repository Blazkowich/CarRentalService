import { Component, OnDestroy, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { IVehicle } from '../models/vehicle.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationBookingDialogComponent } from '../confirmation-popup-window/booking-options/confirmation-booking.component';
import { MatIconModule } from '@angular/material/icon';
import { BookingService } from '../services/booking.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatIconModule]
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
  duration = 1;
  private _listFilter: string = '';


  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
    private bookingService: BookingService,
    public dialog: MatDialog
    ) { }

  ngOnInit(): void {
    this.sub = this.vehicleService.getVehicles().subscribe({
      next: vehicles => {
        this.vehicles = vehicles;
        this.filteredVehicles = this.vehicles;
      },
      error: err => this.errorMessage = err
    });
  }

  get listFilter(): string {
    return this._listFilter;
  }

  set listFilter(value: string) {
    this._listFilter = value.trim().toLocaleLowerCase();
    this.filteredVehicles = this.performFilter(this._listFilter);
  }

  openBookingDialog(vehicleId: string, vehicleName: string): void {
    const dialogRef = this.dialog.open(ConfirmationBookingDialogComponent, {
      width: '600px',
      data: { vehicleId: vehicleId, vehicleName: vehicleName, duration: this.duration }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.bookVehicle(vehicleId, result.duration);
      }
    });
  }

  bookVehicle(vehicleId: string, duration: number): void {
    this.bookingService.bookVehicle(vehicleId, duration).subscribe({
      next: booking => {
        console.log('Booking successful:', booking);
      },
      error: err => {
        console.error('Error booking vehicle:', err);
      }
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
      vehicle.name.toLocaleLowerCase().includes(filterBy)
    );
  }
}
