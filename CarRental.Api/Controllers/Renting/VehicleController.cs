using CarRental.Service.Mapper.DTO.Enum;
using CarRental.Service.Mapper.DTO.Request;
using CarRental.Service.Mapper.DTO.Response;
using CarRental.Service.Mapper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.Renting
{
    [ApiController]
    [Route("vehicles")]
    [Authorize]
    public class VehicleController(IVehicleMapped vehicleMapped) : ControllerBase
    {
        private readonly IVehicleMapped _vehicleMapped = vehicleMapped;

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<VehicleResponseFull>> GetAllVehicles()
        {
            return await _vehicleMapped.GetAllVehiclesAsync();
        }

        [HttpGet("byId/{vehicleId}")]
        [AllowAnonymous]
        public async Task<VehicleResponseFull> GetVehicleById(Guid vehicleId)
        {
            return await _vehicleMapped.GetVehicleByIdAsync(vehicleId);
        }

        [HttpGet("byName/{name}")]
        public async Task<VehicleResponseFull> GetVehicleByName(string name)
        {
            return await _vehicleMapped.GetVehicleByNameAsync(name);
        }

        [HttpGet("byType")]
        [AllowAnonymous]
        public async Task<List<VehicleResponseFull>> GetVehiclesByType([FromQuery] VehicleTypeEnumApi type)
        {
            return await _vehicleMapped.GetVehiclesByTypeAsync(type);
        }

        [HttpGet("available")]
        [AllowAnonymous]
        public async Task<List<VehicleResponseFull>> GetAllAvailableVehicles()
        {
            return await _vehicleMapped.GetAllAvailableVehiclesAsync();
        }

        [HttpGet("reservationCondition")]
        public async Task<List<VehicleResponseFull>> GetVehiclesByReservationType([FromQuery] ReservationTypeApi reservationType)
        {
            return await _vehicleMapped.GetVehiclesByReservationTypeAsync(reservationType);
        }

        [HttpPost]
        public async Task<VehicleResponseFull> AddVehicle([FromBody] VehicleRequest vehicle)
        {
            return await _vehicleMapped.AddVehicleAsync(vehicle);
        }

        [HttpPut]
        public async Task<VehicleResponseFull> UpdateVehicle([FromBody] VehicleRequest vehicle)
        {
            return await _vehicleMapped.UpdateVehicleAsync(vehicle);
        }

        [HttpDelete]
        public async Task DeleteVehicle([FromBody] VehicleRequest vehicle)
        {
            await _vehicleMapped.DeleteVehicleAsync(vehicle);
        }

        [HttpGet("getVehicleTypes")]
        [AllowAnonymous]
        public List<string> GetVehicleTypes()
        {
            return _vehicleMapped.GetVehicleTypes();
        }
    }
}
