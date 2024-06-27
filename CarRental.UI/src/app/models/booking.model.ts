import { IVehicle } from "./vehicle.model";

export interface IBooking {
  Id: string;
  CustomerId: string;
  VehicleId: string;
  BookingDate: Date;
  StartDate: Date;
  EndDate: Date;
  TotalPrice: number;
  BookingCondition: string;
  vehicle: IVehicle;
  duration: number;
}
