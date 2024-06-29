import { IBooking } from './../../models/booking.model';

export function mapBookingApiToApp(booking: any): IBooking {
  return {
    id: booking.Id,
    customerId: booking.CustomerId,
    vehicleId: booking.VehicleId,
    bookingDate: booking.BookingDate,
    startDate: booking.StartDate,
    endDate: booking.EndDate,
    totalPrice: booking.TotalPrice,
    bookingCondition: booking.BookingCondition,
    vehicle: booking.vehicle,
    duration: booking.duration
  };
}
