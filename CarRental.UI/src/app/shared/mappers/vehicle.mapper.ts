import { IVehicle } from "../../models/vehicle.model";

export function mapVehicleApiToApp(vehicle: any): IVehicle {
  return {
    id: vehicle.Id,
    name: vehicle.Name,
    description: vehicle.Description,
    type: vehicle.Type,
    reservationType: vehicle.ReservationType,
    price: vehicle.Price,
    imageUrl: vehicle.ImageUrl
  };
}

