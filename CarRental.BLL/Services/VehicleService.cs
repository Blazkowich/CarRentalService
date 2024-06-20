using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Repositories.RentalUnitOfWork;

namespace CarRental.BLL.Services;

public class VehicleService(IRentalUnitOfWork rentalUnitOfWork, IMapper mapper) : IVehicleService
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        var allVehicles = await _rentalUnitOfWork.VehiclesRepository.GetAllAsync();
        return _mapper.Map<List<Vehicle>>(allVehicles);
    }
}