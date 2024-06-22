using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Context.Entities.Enum;
using CarRental.DAL.Repositories.Interfaces;
using CarRental.DAL.Repositories.RentalUnitOfWork;
using System;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    public class ReservationScheduler : IReservationScheduler
    {
        private readonly IRentalUnitOfWork _rentalUnitOfWork;

        public ReservationScheduler(IRentalUnitOfWork rentalUnitOfWork)
        {
            _rentalUnitOfWork = rentalUnitOfWork;
        }

        public async Task UpdateReservationsAsync()
        {
            var reservations = await _rentalUnitOfWork.BookingsRepository
                .GetBookingsByConditionAsync(BookingTypeDAL.Reserved);

            foreach (var reservation in reservations)
            {
                if (reservation.StartDate <= DateTime.UtcNow)
                {
                    if (DateTime.UtcNow > reservation.EndDate)
                    {
                        reservation.BookingCondition = BookingTypeDAL.Finished;

                        var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(reservation.VehicleId);
                        vehicle.ReservationType = ReservationTypeDAL.Free;

                        await _rentalUnitOfWork.BookingsRepository.UpdateAsync(reservation);
                        await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(vehicle);
                    }
                    else
                    {
                        reservation.BookingCondition = BookingTypeDAL.Active;

                        var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(reservation.VehicleId);
                        vehicle.ReservationType = ReservationTypeDAL.Reserved;
                    }

                    await _rentalUnitOfWork.SaveAsync();
                }
            }
        }
    }
}
