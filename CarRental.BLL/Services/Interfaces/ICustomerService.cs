using CarRental.BLL.Models;

namespace CarRental.BLL.Services.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomersAsync();
}
