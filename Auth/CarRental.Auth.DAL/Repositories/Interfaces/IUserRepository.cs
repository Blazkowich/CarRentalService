using CarRental.Auth.DAL.Context.Entities;
using CarRental.DAL.Common.BaseRepository;

namespace CarRental.Auth.DAL.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<Guid, UserEntity>
{
    Task<UserEntity> GetUserByUserNameAsync(string username, CancellationToken ct = default);
}

