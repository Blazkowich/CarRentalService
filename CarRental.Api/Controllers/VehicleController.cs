using AutoMapper;
using CarRental.Api.ApiModels.Enum;
using CarRental.Api.ApiModels.Request;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Models;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("vehicles")]
[Authorize]
public class VehicleController(IVehicleService vehicleService, IMapper mapper) : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [AllowAnonymous]
    public async Task<List<VehicleResponseFull>> GetAllVehicles()
    {
        var getAllVehicles = await _vehicleService.GetVehiclesAsync();
        return _mapper.Map<List<VehicleResponseFull>>(getAllVehicles);
    }

    [HttpGet("byId/{vehicleId}")]
    [AllowAnonymous]
    public async Task<VehicleResponseFull> GetVehicleById(Guid vehicleId)
    {
        var getById = await _vehicleService.GetVehicleByIdAsync(vehicleId);
        return _mapper.Map<VehicleResponseFull>(getById);
    }

    [HttpGet("byName/{name}")]
    public async Task<VehicleResponseFull> GetVehicleByName(string name)
    {
        var getByName = await _vehicleService.GetVehicleByNameAsync(name);
        return _mapper.Map<VehicleResponseFull>(getByName);
    }

    [HttpGet("byType")]
    [AllowAnonymous]
    public async Task<List<VehicleResponseFull>> GetVehiclesByType([FromQuery] VehicleTypeEnumApi type)
    {
        var getByType = await _vehicleService.GetVehiclesByTypeAsync(_mapper.Map<VehicleTypeBLL>(type));
        return _mapper.Map<List<VehicleResponseFull>>(getByType);
    }

    [HttpGet("available")]
    [AllowAnonymous]
    public async Task<List<VehicleResponseFull>> GetAllAvailableVehicles()
    {
        var getAllAvailable = await _vehicleService.GetAllAvailableVehiclesAsync();
        return _mapper.Map<List<VehicleResponseFull>>(getAllAvailable);
    }

    [HttpGet("reservationCondition")]
    public async Task<List<VehicleResponseFull>> GetVehiclesByReservationType([FromQuery] ReservationTypeApi reservationType)
    {
        var getVehiclesByReservation = await _vehicleService
            .GetVehiclesByReservationTypeAsync(_mapper.Map<ReservationTypeBLL>(reservationType));

        return _mapper.Map<List<VehicleResponseFull>>(getVehiclesByReservation);
    }

    [HttpPost]
    public async Task<VehicleResponseFull> AddVehicle([FromBody] VehicleRequest vehicle)
    {
        var newVehicle = await _vehicleService.AddVehicleAsync(_mapper.Map<Vehicle>(vehicle));
        return _mapper.Map<VehicleResponseFull>(newVehicle);
    }

    [HttpPut]
    public async Task<VehicleResponseFull> UpdateVehicle([FromBody] VehicleRequest vehicle)
    {
        var updatedVehicle = await _vehicleService.UpdateVehicleAsync(_mapper.Map<Vehicle>(vehicle));
        return _mapper.Map<VehicleResponseFull>(updatedVehicle);
    }

    [HttpDelete]
    public async Task DeleteVehicle([FromBody] VehicleRequest vehicle)
    {
        await _vehicleService.DeleteVehicleAsync(vehicle.Id);
    }
    //add vehicle

    //update vehicle

    //remove vehicle
}
