﻿using CarRental.DAL.Common.Repositories.Interfaces;
using CarRental.DAL.Context.Entities;

namespace CarRental.DAL.Repositories.Interfaces;

public interface IVehicleRepository : IBaseRepository<Guid, VehicleEntity>
{
}
