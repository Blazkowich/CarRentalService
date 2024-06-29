using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.AuthUnitOfWork;
using CarRental.DAL.Common.Paging;
using CarRental.Shared.CustomExceptions;

namespace CarRental.Auth.BLL.Services;

internal class UserService(
    IAuthUnitOfWork unitOfWork,
    IRolesService roleService,
    IMapper mapper) : IUserService
{
    private readonly IAuthUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRolesService _roleService = roleService;
    private readonly IMapper _mapper = mapper;

    #region Need To Maintain these methods
    public async Task<Guid> AddUserAsync(User user)
    {
        await ValidateUserOnAdding(user);

        var createdUser = await _unitOfWork.UserRepository.AddAsync(_mapper.Map<UserEntity>(user));
        await _unitOfWork.SaveAsync();

        return createdUser.Id;
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new BadRequestException("Invalid userId Format");
        }

        _ = await _unitOfWork.UserRepository.GetByIdAsync(userId) ??
            throw new NotFoundException($"User with Id {userId} Not Found.");

        await _unitOfWork.UserRepository.DeleteAsync(userId);

        await _unitOfWork.SaveAsync();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await ValidateUserOnUpdating(user);

        var result = await _unitOfWork.UserRepository.UpdateAsync(_mapper.Map<UserEntity>(user));

        return _mapper.Map<User>(result);
    }
    #endregion

    public async Task<List<User>> GetAllUsersAsync()
    {
        var searchUserContext = new EntitiesPagingRequest<UserEntity>
        {
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var searchResult = await _unitOfWork.UserRepository.SearchWithPagingAsync(searchUserContext);

        return _mapper.Map<List<User>>(searchResult.Items);
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new BadRequestException("User Id Is Null");
        }

        var getUserById = await _unitOfWork.UserRepository.GetByIdAsync(id);

        return _mapper.Map<User>(getUserById) ?? throw new NotFoundException("User Not Found");
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new BadRequestException("User name Is Null or Empty");
        }

        var getUserByName = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName, default);

        return _mapper.Map<User>(getUserByName) ?? throw new NotFoundException("User Not Found");
    }

    public async Task<List<Roles>> GetRolesByUserIdAsync(Guid userId)
    {
        var getById = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (userId == Guid.Empty || getById == null)
        {
            throw new BadRequestException("Invalid User");
        }

        var getUserRoles = await _unitOfWork.RolesRepository.GetRolesByUserIdAsync(userId);

        return _mapper.Map<List<Roles>>(getUserRoles);
    }

    public async Task<List<Permissions>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            throw new BadRequestException("Invalid RoleId");
        }

        var permissionsEntities = await _unitOfWork.PermissionsRepository.GetPermissionsByRoleIdAsync(roleId);

        var permissions = permissionsEntities.Select(pe =>
            new Permissions
            {
                Id = pe.Id.ToString(),
                Name = pe.Name
            }).ToList();

        return permissions;
    }

    #region Private Methods
    private async Task<List<string>> GetPermissionsForRolesAsync(List<string> roles)
    {
        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var roleSearchContext = new EntitiesPagingRequest<RolesEntity>
            {
                Filter = x => x.Name == role,
                PageNumber = 1,
                PerPage = int.MaxValue,
            };

            var getRole = await _unitOfWork.RolesRepository.SearchWithPagingAsync(roleSearchContext);

            var rolePermissions = await _roleService.GetPermissionsByRoleIdAsync(getRole.Items.FirstOrDefault().Id);
            permissions.AddRange(rolePermissions);
        }

        return permissions;
    }

    private async Task ValidateUserOnAdding(User user)
    {
        if (user == null)
        {
            throw new BadRequestException("Invalid User Model");
        }

        foreach (var role in user.Roles)
        {
            var roleEntity = await _unitOfWork.RolesRepository.GetByIdAsync(Guid.Parse(role.Id));
            if (roleEntity == null || roleEntity.Id == Guid.Empty)
            {
                throw new BadRequestException("One or more specified roles do not exist.");
            }
        }
    }

    private async Task ValidateUserOnUpdating(User user)
    {
        if (user == null)
        {
            throw new BadRequestException("Invalid User Model");
        }

        _ = await _unitOfWork.UserRepository.GetByIdAsync(Guid.Parse(user.Id)) ??
            throw new BadRequestException("User does not exist");

        foreach (var role in user.Roles)
        {
            var roleEntity = await _unitOfWork.RolesRepository.GetByIdAsync(Guid.Parse(role.Id));
            if (roleEntity == null || roleEntity.Id == Guid.Empty)
            {
                throw new BadRequestException("One or more specified roles do not exist.");
            }
        }
    }
    #endregion
}

