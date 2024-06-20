using AutoMapper;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("vehicle")]
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

    //get all available vehicles

    //get booked vehicles

    //get overdued vehicles

    //get vehicle by id

    //get vehicle by name

    //get vehicle by user

    //add vehicle

    //update vehicle

    //remove vehicle
}
