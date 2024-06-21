using CarRental.Auth.DAL.Context.Entities;
using CarRental.DAL.Common.Repositories.Interfaces;

namespace CarRental.Auth.DAL.Repositories.Interfaces;

public interface IUserRoleRepository : IBaseRepository<Guid, UserRolesEntity>
{
}
