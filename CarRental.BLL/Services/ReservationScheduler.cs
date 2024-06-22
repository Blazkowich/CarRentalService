using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Context.Entities.Enum;
using CarRental.DAL.Repositories.RentalUnitOfWork;

namespace CarRental.BLL.Services;

public class ReservationScheduler(IRentalUnitOfWork rentalUnitOfWork) : IReservationScheduler
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;

    public async Task UpdateReservationsAsync()
    {
        var reservations = await _rentalUnitOfWork.BookingsRepository
            .GetBookingsByConditionAsync(BookingTypeDAL.Reserved);

        foreach (var reservation in reservations)
        {
            if (reservation.StartDate <= DateTime.UtcNow)
            {
                reservation.BookingCondition = BookingTypeDAL.Active;

                var vehicle = await _rentalUnitOfWork.VehiclesRepository.GetByIdAsync(reservation.VehicleId);
                vehicle.ReservationType = ReservationTypeDAL.Reserved;

                await _rentalUnitOfWork.BookingsRepository.UpdateAsync(reservation);
                await _rentalUnitOfWork.VehiclesRepository.UpdateAsync(vehicle);
                await _rentalUnitOfWork.SaveAsync();
            }
        }
    }
}
