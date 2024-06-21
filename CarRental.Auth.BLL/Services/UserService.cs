using Api.Bootstrapping.CustomExceptions;
using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.AuthUnitOfWork;
using CarRental.DAL.Common.Paging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarRental.Auth.BLL.Services;

internal class UserService(
    IAuthUnitOfWork unitOfWork,
    ITokenService tokenService,
    IRolesService roleService,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper) : IUserService
{
    private readonly IAuthUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IRolesService _roleService = roleService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMapper _mapper = mapper;

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

        var getRolesPermissions = await _unitOfWork.PermissionsRepository.GetPermissionsByRoleIdAsync(roleId);

        return _mapper.Map<List<Permissions>>(getRolesPermissions);
    }

    public async Task<string> Login(User user)
    {
        var userEntity = await GetUserByNameAsync(user.Name);
        ValidateUserPassword(user, userEntity);

        var userRoles = await GetRolesByUserIdAsync(userEntity.Id);
        var rolesEntities = _mapper.Map<List<RolesEntity>>(userRoles);
        var userPermissions = await GetUserPermissionsAsync(rolesEntities);

        var refreshToken = await GenerateUserTokenAsync(user, rolesEntities, userPermissions);

        var refreshedToken = SetRefreshToken(userEntity, refreshToken);

        await _unitOfWork.SaveAsync();

        return refreshedToken;
    }

    public async Task<(User UserEntity, string Token)> Register(User user)
    {
        var userEntity = CreateUserEntity(user);

        var userRole = await GetUserRoleAsync("User") ??
            throw new BadRequestException("Default role not found.");

        await AssignRoleToUserAsync(userEntity, userRole);
        await _unitOfWork.SaveAsync();

        var rolesEntities = new List<RolesEntity> { userRole };
        var userPermissions = await GetUserPermissionsAsync(rolesEntities);
        var token = await GenerateUserTokenAsync(user, rolesEntities, userPermissions);

        return (_mapper.Map<User>(userEntity), token.Token);
    }

    public async Task<bool> CheckPageAccess(string targetPage, string targetId, ClaimsPrincipal user)
    {
        var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        var permissions = await GetPermissionsForRolesAsync(roles);

        var requiredPermission = new List<string>
        {
            $"{targetPage}.{targetId}.ReadOnly",
            $"{targetPage}.{targetId}.ReadWrite",
        };

        var replacedPermissions = requiredPermission.Select(p => p.Replace('.', ' ')).ToList();

        return replacedPermissions.Any(rp => permissions.Any(p => rp.Contains(p)));
    }

    #region Login And Registration Private Methods
    private async Task<UserEntity> GetUserByNameAsync(string userName)
    {
        var userSearchContext = new EntitiesPagingRequest<UserEntity>
        {
            Filter = x => x.Name == userName,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var userResult = await _unitOfWork.UserRepository.SearchWithPagingAsync(userSearchContext);
        var userEntity = userResult.Items.FirstOrDefault() ?? throw new NotFoundException("User Not Found");

        return userEntity;
    }

    private void ValidateUserPassword(User user, UserEntity userEntity)
    {
        if (!_tokenService.VerifyPasswordHash(user.Password, userEntity.PasswordHash, userEntity.PasswordSalt))
        {
            throw new BadRequestException("Incorrect Password");
        }
    }

    private UserEntity CreateUserEntity(User user)
    {
        _tokenService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

        return new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
    }

    private async Task<RolesEntity?> GetUserRoleAsync(string roleName)
    {
        var searchUserRoleContext = new EntitiesPagingRequest<RolesEntity>
        {
            Filter = x => x.Name == roleName,
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var userRole = await _unitOfWork.RolesRepository.SearchWithPagingAsync(searchUserRoleContext);

        return userRole.Items.FirstOrDefault();
    }

    private async Task AssignRoleToUserAsync(UserEntity userEntity, RolesEntity userRole)
    {
        var userRoleEntity = new UserRolesEntity
        {
            User = userEntity,
            Role = userRole,
        };

        await _unitOfWork.UserRepository.AddAsync(userEntity);
        await _unitOfWork.UserRoleRepository.AddAsync(userRoleEntity);
    }

    private async Task<List<Permissions>> GetUserPermissionsAsync(List<RolesEntity> userRoles)
    {
        var permissionTasks = userRoles
            .Select(role => GetPermissionsByRoleIdAsync(role.Id))
            .ToList();

        await Task.WhenAll(permissionTasks);

        return permissionTasks.SelectMany(task => task.Result).ToList();
    }

    private async Task<RefreshToken> GenerateUserTokenAsync(User user, List<RolesEntity> rolesEntities, List<Permissions> userPermissions)
    {
        var roles = rolesEntities.Select(_mapper.Map<Roles>).ToList();
        return await _tokenService.CreateOrGenerateRefreshToken(user, roles, userPermissions);
    }

    #endregion

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

    private string SetRefreshToken(UserEntity user, RefreshToken newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires,
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("Bearer", newRefreshToken.Token, cookieOptions);

        user.RefreshToken = newRefreshToken.Token;
        user.TokenCreated = newRefreshToken.Created;
        user.TokenExpires = newRefreshToken.Expires;

        return newRefreshToken.Token;
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

