using AutoMapper;
using CarRental.Api.ApiModels.Enum;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Models.Enum;
using CarRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("vehicles")]
public class VehicleController(IVehicleService vehicleService, IMapper mapper) : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<List<VehicleResponseFull>> GetAllVehicles()
    {
        var getAllVehicles = await _vehicleService.GetVehiclesAsync();
        return _mapper.Map<List<VehicleResponseFull>>(getAllVehicles);
    }

    [HttpGet("byId/{vehicleId}")]
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
    public async Task<List<VehicleResponseFull>> GetVehiclesByType([FromQuery] VehicleTypeEnumApi type)
    {
        var getByType = await _vehicleService.GetVehiclesByTypeAsync(_mapper.Map<VehicleTypeBLL>(type));
        return _mapper.Map<List<VehicleResponseFull>>(getByType);
    }

    //get all available vehicles

    //get booked vehicles

    //get overdued vehicles

    //get vehicle by user

    //add vehicle

    //update vehicle

    //remove vehicle
}
