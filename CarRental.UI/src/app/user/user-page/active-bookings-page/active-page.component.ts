import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { IVehicle } from '../../../models/vehicle.model';
import { IBooking } from '../../../models/booking.model';
import { VehicleService } from '../../../services/vehicle.service';
import { AuthService } from '../../../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { BookingService } from '../../../services/booking.service';
import { ConfirmationDialogComponent } from '../../../confirmation-popup-window/removing-cancel-confirmation/confirmation.component';

@Component({
  selector: 'app-active-renting-page',
  templateUrl: './active-page.component.html',
  styleUrls: ['./active-page.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})
export class ActiveRentingPageComponent implements OnInit {
  title = 'Active Rentings';
  activeBookingDetails: ActiveBookingDetail[] = [];
  userId: string | null = null;
  imageWidth = 200;
  imageMargin = 0;

  constructor(
    private vehicleService: VehicleService,
    private authService: AuthService,
    private bookingService: BookingService,
    private router: Router,
    private dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    if (this.userId) {
      this.getActiveBookings(this.userId);
    } else {
      console.error('User ID not found.');
    }
  }

  getActiveBookings(userId: string): void {
    this.bookingService.getActiveBooking(userId).subscribe(
      (bookings: IBooking[]) => {
        const bookingDetailsPromises = bookings.map(booking => this.fetchVehicleDetailsForBooking(booking));
        Promise.all(bookingDetailsPromises).then(bookingDetails => {
          this.activeBookingDetails = bookingDetails;
        }).catch(error => {
          console.error('Error fetching vehicle details:', error);
        });
      },
      (error: any) => {
        console.error('Error fetching active bookings:', error);
      }
    );
  }

  fetchVehicleDetailsForBooking(booking: IBooking): Promise<ActiveBookingDetail> {
    return new Promise<ActiveBookingDetail>((resolve, reject) => {
      this.vehicleService.getVehicleById(booking.vehicleId).subscribe(
        (vehicle: IVehicle | undefined) => {
          if (vehicle) {
            const bookingDetail: ActiveBookingDetail = {
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

  cancelBookingOfVehicle(booking: IBooking): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { booking: booking, message: "Do you really want to cancel booking?" }
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.bookingService.cancelBooking(booking).subscribe(
          () => {
            this.activeBookingDetails = this.activeBookingDetails.filter(b => b.booking.id !== booking.id);
            console.log('Cancelled booking for vehicle ID', booking.vehicleId);
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

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  onBack(): void {
    this.router.navigate(['/main-page']);
  }
}

interface ActiveBookingDetail {
  booking: IBooking;
  vehicleName?: string;
  vehicleImageUrl?: string;
}
