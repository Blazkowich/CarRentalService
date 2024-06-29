using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using CarRental.Service.Mapper.DTO.Enum;
using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.DTO.Response;
using CarRental.Service.Mapper.Services.Interfaces;

namespace CarRental.Service.Mapper.Services
{
    internal class VehicleMapped : IVehicleMapped
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleMapped(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<List<VehicleResponseFull>> GetAllVehiclesAsync()
        {
            var getAllVehicles = await _vehicleService.GetVehiclesAsync();
            return _mapper.Map<List<VehicleResponseFull>>(getAllVehicles);
        }

        public async Task<VehicleResponseFull> GetVehicleByIdAsync(Guid vehicleId)
        {
            var getById = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            return _mapper.Map<VehicleResponseFull>(getById);
        }

        public async Task<VehicleResponseFull> GetVehicleByNameAsync(string name)
        {
            var getByName = await _vehicleService.GetVehicleByNameAsync(name);
            return _mapper.Map<VehicleResponseFull>(getByName);
        }

        public async Task<List<VehicleResponseFull>> GetVehiclesByTypeAsync(VehicleTypeEnumApi type)
        {
            var getByType = await _vehicleService.GetVehiclesByTypeAsync(_mapper.Map<VehicleTypeBLL>(type));
            return _mapper.Map<List<VehicleResponseFull>>(getByType);
        }

        public async Task<List<VehicleResponseFull>> GetAllAvailableVehiclesAsync()
        {
            var getAllAvailable = await _vehicleService.GetAllAvailableVehiclesAsync();
            return _mapper.Map<List<VehicleResponseFull>>(getAllAvailable);
        }

        public async Task<List<VehicleResponseFull>> GetVehiclesByReservationTypeAsync(ReservationTypeApi reservationType)
        {
            var getVehiclesByReservation = await _vehicleService
                .GetVehiclesByReservationTypeAsync(_mapper.Map<ReservationTypeBLL>(reservationType));

            return _mapper.Map<List<VehicleResponseFull>>(getVehiclesByReservation);
        }

        public async Task<VehicleResponseFull> AddVehicleAsync(VehicleRequest vehicle)
        {
            var newVehicle = await _vehicleService.AddVehicleAsync(_mapper.Map<Vehicle>(vehicle));
            return _mapper.Map<VehicleResponseFull>(newVehicle);
        }

        public async Task<VehicleResponseFull> UpdateVehicleAsync(VehicleRequest vehicle)
        {
            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(_mapper.Map<Vehicle>(vehicle));
            return _mapper.Map<VehicleResponseFull>(updatedVehicle);
        }

        public async Task DeleteVehicleAsync(VehicleRequest vehicle)
        {
            await _vehicleService.DeleteVehicleAsync(vehicle.Id);
        }

        public List<string> GetVehicleTypes()
        {
            return _vehicleService.GetEnumToString(typeof(VehicleTypeEnumApi));
        }
    }
}
