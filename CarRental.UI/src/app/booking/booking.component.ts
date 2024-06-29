import { Component, OnInit } from "@angular/core";
import { BookingService } from "../services/booking.service";
import { IBooking } from "../models/booking.model";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class BookingComponent implements OnInit {
  vehicleId: string = '';
  vehicleName: string = '';
  startDate: Date = new Date();
  duration: number = 1;

  constructor(
    private route: ActivatedRoute,
    private bookingService: BookingService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.vehicleId = params['id'];
    });
  }

  reserveVehicle(): void {
    this.bookingService.reserveVehicle(this.vehicleId, this.startDate, this.duration).subscribe(
      (response: IBooking) => {
        console.log('Reservation successful:', response);
      },
      (error) => {
        console.error('Error reserving vehicle:', error);
      }
    );
  }

  bookVehicle(): void {
    this.bookingService.bookVehicle(this.vehicleId, this.duration).subscribe(
      (response: IBooking) => {
        console.log('Booking successful:', response);
      },
      (error) => {
        console.error('Error booking vehicle:', error);
      }
    );
  }
}
