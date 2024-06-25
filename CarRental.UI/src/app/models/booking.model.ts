export interface IBooking {
  Id: string;
  CustomerId: string;
  VehicleId: string;
  BookingDate: Date;
  StartDate: Date;
  EndDate: Date;
  TotalPrice: number;
  BookingCondition: string;
}
