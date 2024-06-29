import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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
  bookingHistoryDetails: HistoryBookingDetail[] = [];
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
        const bookingDetailsPromises = bookings.map(booking => this.fetchVehicleDetailsForBooking(booking));
        Promise.all(bookingDetailsPromises).then(bookingDetails => {
          this.bookingHistoryDetails = bookingDetails;
        }).catch(error => {
          console.error('Error fetching vehicle details:', error);
        });
      },
      (error: any) => {
        console.error('Error fetching booking history:', error);
      }
    );
  }

  fetchVehicleDetailsForBooking(booking: IBooking): Promise<HistoryBookingDetail> {
    return new Promise<HistoryBookingDetail>((resolve, reject) => {
      this.vehicleService.getVehicleById(booking.vehicleId).subscribe(
        (vehicle: IVehicle | undefined) => {
          if (vehicle) {
            const bookingDetail: HistoryBookingDetail = {
              booking: booking,
              vehicleName: vehicle.name,
              vehicleImageUrl: vehicle.imageUrl
            };
            resolve(bookingDetail);
          } else {
            reject(`Vehicle details not found for booking ${booking.id}`);
          }
        },
        (error: any) => {
          reject(`Error fetching vehicle details for booking ${booking.id}: ${error}`);
        }
      );
    });
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  getVehicleImageUrl(vehicleId: string): string | undefined {
    const bookingDetail = this.bookingHistoryDetails.find(detail => detail.booking.vehicleId === vehicleId);
    return bookingDetail ? bookingDetail.vehicleImageUrl : undefined;
  }

  getVehicleName(vehicleId: string): string | undefined {
    const bookingDetail = this.bookingHistoryDetails.find(detail => detail.booking.vehicleId === vehicleId);
    return bookingDetail ? bookingDetail.vehicleName : 'Vehicle Name Not Available';
  }
}

interface HistoryBookingDetail {
  booking: IBooking;
  vehicleName?: string;
  vehicleImageUrl?: string;
}
