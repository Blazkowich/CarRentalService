using CarRental.DAL.Common.BaseRepository;
using CarRental.DAL.Context.Entities;

namespace CarRental.DAL.Repositories.Interfaces;

public interface ICustomerRepository : IBaseRepository<Guid, CustomerEntity>
{
}
