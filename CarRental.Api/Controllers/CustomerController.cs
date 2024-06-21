using AutoMapper;
using CarRental.Api.ApiModels.Response;
using CarRental.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("customers")]
public class CustomerController(ICustomerService customerService, IMapper mapper) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<List<CustomerResponseFull>> GetAllCustomers()
    {
        var customerResponse = await _customerService.GetAllCustomersAsync();
        return _mapper.Map<List<CustomerResponseFull>>(customerResponse);
    }
}
