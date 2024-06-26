import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/vehicle.model';
import { IBooking } from '../../models/booking.model';
import { VehicleService } from '../../services/vehicle.service';
import { AuthService } from '../../services/auth.service';
import { UserPageService } from '../../services/user-page.service';
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
  bookingHistory: IBooking[] = [];
  activeBookings: IBooking[] = [];
  userId: string | null = null;
  imageWidth = 200;
  imageMargin = 0;

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
    private userPageService: UserPageService
  ) {}

  ngOnInit(): void {
    this.getAvailableVehicles();
    this.userId = this.authService.getUserId();
    if (this.userId) {
      this.getBookingHistory(this.userId);
      this.getActiveBookings(this.userId);
    } else {
      console.error('User ID not found.');
    }
  }

  getBookingHistory(userId: string): void {
    this.userPageService.getBookingHistory(userId).subscribe(
      (bookings: IBooking[]) => {
        this.bookingHistory = bookings;
        this.fetchVehicleDetailsForBookingHistory();
      },
      (error: any) => {
        console.error('Error fetching booking history:', error);
      }
    );
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

  fetchVehicleDetailsForBookingHistory(): void {
    this.vehicles = [];

    for (const booking of this.bookingHistory) {
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

}
