import { Component, OnInit } from '@angular/core';
import { CommonModule, formatDate } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { IVehicle } from '../../../models/vehicle.model';
import { IBooking } from '../../../models/booking.model';
import { VehicleService } from '../../../services/vehicle.service';
import { AuthService } from '../../../services/auth.service';
import { UserPageService } from '../../../services/user-page.service';
import { MatDialog } from '@angular/material/dialog';
import { CancelConfirmationDialogComponent } from '../../../confirmation-popup-window/booking-cancel/cancel-confirmation.component';
import { BookingService } from '../../../services/booking.service';

@Component({
  selector: 'app-active-renting-page',
  templateUrl: './active-page.component.html',
  styleUrls: ['./active-page.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class ActiveRentingPageComponent implements OnInit {
  title = 'Active Rentings';
  availableVehicles: IVehicle[] = [];
  vehicles: IVehicle[] = []
  activeBookings: IBooking[] = [];
  userId: string | null = null;
  imageWidth = 200;
  imageMargin = 0;

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
    private userPageService: UserPageService,
    private router: Router,
    private dialog: MatDialog,
    private bookingService: BookingService
  ) {}

  ngOnInit(): void {
    this.getAvailableVehicles();
    this.userId = this.authService.getUserId();
    if (this.userId) {
      this.getActiveBookings(this.userId);
    } else {
      console.error('User ID not found.');
    }
  }

  getActiveBookings(userId: string): void {
    this.userPageService.getActiveBooking(userId).subscribe(
      (bookings: IBooking[]) => {
        this.activeBookings = bookings;
        this.fetchVehicleDetailsForActiveBookings();
      },
      (error: any) => {
        console.error('Error fetching active bookings:', error);
      }
    );
  }

  fetchVehicleDetailsForActiveBookings(): void {
    this.vehicles = [];

    for (const booking of this.activeBookings) {
      this.vehicleService.getVehicleById(booking.VehicleId).subscribe(
        (vehicle: IVehicle | undefined) => {
          if (vehicle) {
            this.vehicles.push(vehicle);
          } else {
            console.error(`Vehicle details not found for booking ${booking.Id}`);
          }
        },
        (error: any) => {
          console.error(`Error fetching vehicle details for booking ${booking.Id}:`, error);
        }
      );
    }
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

  cancelVehicle(booking: IBooking): void {
    const dialogRef = this.dialog.open(CancelConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.bookingService.cancelBooking(booking).subscribe(
          () => {
            this.activeBookings = this.activeBookings.filter(b => b.Id !== booking.Id);
            this.vehicles = this.vehicles.filter(v => v.Id !== booking.VehicleId);
            console.log('Cancelled booking for vehicle ID', booking.VehicleId);
          },
          (error: any) => {
            console.error('Error cancelling booking:', error);
          }
        );
      }
    });
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  getVehicleImageUrl(vehicleId: string): string | undefined {
    const vehicle = this.vehicles.find(v => v.Id === vehicleId);
    return vehicle ? vehicle.ImageUrl : undefined;
  }

  getVehicleName(vehicleId: string): string | undefined {
    const vehicle = this.vehicles.find(v => v.Id === vehicleId);
    return vehicle ? vehicle.Name : 'Vehicle Name Not Available';
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}
