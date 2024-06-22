using CarRental.Auth.DAL.Context.Entities;
using CarRental.DAL.Common.BaseRepository;

namespace CarRental.Auth.DAL.Repositories.Interfaces;

public interface IUserRoleRepository : IBaseRepository<Guid, UserRolesEntity>
{
}
