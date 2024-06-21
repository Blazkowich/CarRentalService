using CarRental.Auth.DAL.Context;
using CarRental.Auth.DAL.Repositories.Interfaces;
using CarRental.DAL.Common.BaseUnitOfWork;

namespace CarRental.Auth.DAL.Repositories.AuthUnitOfWork;

internal class AuthUnitOfWork(
    CarRentalAuthDbContext authDbContext,
    IUserRepository userRepository,
    IRolesRepository rolesRepository,
    IPermissionsRepository permissionsRepository,
    IUserRoleRepository userRoleRepository) :
    BaseUnitOfWork(authDbContext), IAuthUnitOfWork
{
    private readonly CarRentalAuthDbContext _authDbContext;

    public IUserRepository UserRepository { get; private set; } = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public IRolesRepository RolesRepository { get; private set; } = rolesRepository ?? throw new ArgumentNullException(nameof(rolesRepository));

    public IPermissionsRepository PermissionsRepository { get; private set; } = permissionsRepository ?? throw new ArgumentNullException(nameof(permissionsRepository));

    public IUserRoleRepository UserRoleRepository { get; private set; } = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
}

