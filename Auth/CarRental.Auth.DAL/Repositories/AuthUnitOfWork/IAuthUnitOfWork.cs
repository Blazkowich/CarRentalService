using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.BaseUnitOfWork;

namespace CarRental.Auth.DAL.Repositories.AuthUnitOfWork;

public interface IAuthUnitOfWork : IBaseUnitOfWork
{
    IUserRepository UserRepository { get; }

    IRolesRepository RolesRepository { get; }

    IPermissionsRepository PermissionsRepository { get; }

    IUserRoleRepository UserRoleRepository { get; }
}

