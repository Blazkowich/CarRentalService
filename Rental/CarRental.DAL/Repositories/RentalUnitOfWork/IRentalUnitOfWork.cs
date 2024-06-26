﻿using CarRental.DAL.Common.BaseUnitOfWork;
using CarRental.DAL.Repositories.Interfaces;

namespace CarRental.DAL.Repositories.RentalUnitOfWork;

public interface IRentalUnitOfWork : IBaseUnitOfWork
{
    IVehicleRepository VehiclesRepository { get; }

    IBookingRepository BookingsRepository { get; }
}
