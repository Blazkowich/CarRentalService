import { IVehicle } from "./vehicle.model";

export interface IBooking {
  id: string;
  customerId: string;
  vehicleId: string;
  bookingDate: Date;
  startDate: Date;
  endDate: Date;
  totalPrice: number;
  bookingCondition: string;
  vehicle: IVehicle;
  duration: number;
}
