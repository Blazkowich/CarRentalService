using AutoMapper;
using CarRental.BLL.Models;
using CarRental.BLL.Services.Interfaces;
using CarRental.DAL.Repositories.RentalUnitOfWork;

namespace CarRental.BLL.Services;

internal class CustomerService(IRentalUnitOfWork rentalUnitOfWork, IMapper mapper) : ICustomerService
{
    private readonly IRentalUnitOfWork _rentalUnitOfWork = rentalUnitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        var getAllCustomers = await _rentalUnitOfWork.CustomersRepository.GetAllAsync();
        return _mapper.Map<List<Customer>>(getAllCustomers);
    }
}
