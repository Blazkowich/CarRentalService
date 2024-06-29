import { Component, OnInit } from '@angular/core';
import { CommonModule, formatDate } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { IVehicle } from '../../../models/vehicle.model';
import { IBooking } from '../../../models/booking.model';
import { VehicleService } from '../../../services/vehicle.service';
import { AuthService } from '../../../services/auth.service';
import { BookingService } from '../../../services/booking.service';

@Component({
  selector: 'app-history-renting-page',
  templateUrl: './history-page.component.html',
  styleUrls: ['./history-page.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class HistoryRentingPageComponent implements OnInit {
  title = 'History Rentings';
  vehicles: IVehicle[] = []
  bookingHistory: IBooking[] = [];
  userId: string | null = null;
  imageWidth = 200;
  imageMargin = 0;

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
    private bookingService: BookingService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    if (this.userId) {
      this.getBookingHistory(this.userId);
    } else {
      console.error('User ID not found.');
    }
  }

  getBookingHistory(userId: string): void {
    this.bookingService.getBookingHistory(userId).subscribe(
      (bookings: IBooking[]) => {
        this.bookingHistory = bookings;
        this.fetchVehicleDetailsForBookingHistory();
      },
      (error: any) => {
        console.error('Error fetching booking history:', error);
      }
    );
  }

  fetchVehicleDetailsForBookingHistory(): void {
    this.vehicles = [];
    for (const booking of this.bookingHistory) {
      this.vehicleService.getVehicleById(booking.vehicleId).subscribe(
        (vehicle: IVehicle | undefined) => {
          if (vehicle) {
            this.vehicles.push(vehicle);
          } else {
            console.error(`Vehicle details not found for booking ${booking.id}`);
          }
        },
        (error: any) => {
          console.error(`Error fetching vehicle details for booking ${booking.id}:`, error);
        }
      );
    }
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  getVehicleImageUrl(vehicleId: string): string | undefined {
    const vehicle = this.vehicles.find(v => v.id === vehicleId);
    return vehicle ? vehicle.imageUrl : undefined;
  }

  getVehicleName(vehicleId: string): string | undefined {
    const vehicle = this.vehicles.find(v => v.id === vehicleId);
    return vehicle ? vehicle.name : 'Vehicle Name Not Available';
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }
}
