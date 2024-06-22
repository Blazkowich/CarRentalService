using Api.Bootstrapping.CustomExceptions;
using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.AuthUnitOfWork;
using CarRental.DAL.Common.Paging;
using Microsoft.AspNetCore.Http;

namespace CarRental.Auth.BLL.Services;

internal class AuthService(
    IUserService userService,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor,
    IAuthUnitOfWork unitOfWork,
    IMapper mapper) : IAuthService
{

    private readonly IUserService _userService = userService;
    private readonly IAuthUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMapper _mapper = mapper;
    public async Task<string> Login(User user)
    {
        var userEntity = await GetUserByNameAsync(user.Name);
        ValidateUserPassword(user, userEntity);

        var userRoles = await _userService.GetRolesByUserIdAsync(userEntity.Id);
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

        var rolesEntities = new List<RolesEntity> { userRole };
        var userPermissions = await GetUserPermissionsAsync(rolesEntities);
        var token = await GenerateUserTokenAsync(user, rolesEntities, userPermissions);

        await _unitOfWork.SaveAsync();

        return (_mapper.Map<User>(userEntity), token.Token);
    }

    #region Private Methods
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
            .Select(role => _userService.GetPermissionsByRoleIdAsync(role.Id))
            .ToList();

        await Task.WhenAll(permissionTasks);

        return permissionTasks.SelectMany(task => task.Result).ToList();
    }

    private async Task<RefreshToken> GenerateUserTokenAsync(User user, List<RolesEntity> rolesEntities, List<Permissions> userPermissions)
    {
        var roles = rolesEntities.Select(entity => new Roles
        {
            Id = entity.Id.ToString(),
            Name = entity.Name
        }).ToList();

        return await _tokenService.CreateOrGenerateRefreshToken(user, roles, userPermissions);
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
    #endregion
}
