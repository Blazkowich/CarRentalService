using CarRental.Shared.CustomExceptions;
using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Auth.DAL.Context.Entities;
using CarRental.Auth.DAL.Repositories.AuthUnitOfWork;
using CarRental.DAL.Common.Paging;

namespace CarRental.Auth.BLL.Services;

internal class RolesService(IAuthUnitOfWork unitOfWork, IMapper mapper) : IRolesService
{
    private readonly IAuthUnitOfWork _unitOfWork = unitOfWork;

    private readonly IMapper _mapper = mapper;

    public async Task<Guid> AddRoleAsync(Roles role)
    {
        await ValidateRolesOnAdding(role);

        var createdRole = await _unitOfWork.RolesRepository.AddAsync(_mapper.Map<RolesEntity>(role));
        await _unitOfWork.SaveAsync();

        return createdRole.Id;
    }

    public async Task<Roles> UpdateRoleAsync(Roles role)
    {
        await ValidateRoleOnUpdating(role);

        var updateRole = await _unitOfWork.RolesRepository.UpdateAsync(_mapper.Map<RolesEntity>(role));
        await _unitOfWork.SaveAsync();

        return _mapper.Map<Roles>(updateRole);
    }

    public async Task<List<Roles>> GetAllRolesAsync()
    {
        var searchRolesContext = new EntitiesPagingRequest<RolesEntity>
        {
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var searchResult = await _unitOfWork.RolesRepository.SearchWithPagingAsync(searchRolesContext);

        return _mapper.Map<List<Roles>>(searchResult.Items);
    }

    public async Task<Roles> GetRoleByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new BadRequestException("Role Id Is Null");
        }

        var getRoleById = await _unitOfWork.RolesRepository.GetByIdAsync(id);

        return _mapper.Map<Roles>(getRoleById) ?? throw new NotFoundException("Role Not Found");
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            throw new BadRequestException("Invalid roleId Format");
        }

        _ = await _unitOfWork.RolesRepository.GetByIdAsync(roleId) ??
            throw new NotFoundException($"Role with Id {roleId} Not Found.");

        await _unitOfWork.RolesRepository.DeleteAsync(roleId);

        await _unitOfWork.SaveAsync();
    }

    public async Task<List<string>> GetAllPermissionsAsync()
    {
        var searchPermissionContext = new EntitiesPagingRequest<PermissionEntity>
        {
            PageNumber = 1,
            PerPage = int.MaxValue,
        };

        var searchResult = await _unitOfWork.PermissionsRepository.SearchWithPagingAsync(searchPermissionContext);

        return searchResult.Items.Select(permission => permission.Name).ToList();
    }

    public async Task<List<string>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            throw new BadRequestException("Role ID is null or empty");
        }

        var permissions = await _unitOfWork.PermissionsRepository.GetPermissionsByRoleIdAsync(roleId);

        return permissions
            .Select(p => p.Name)
            .Where(name => !string.IsNullOrEmpty(name))
            .ToList();
    }

    #region Private Methods
    private async Task ValidateRolesOnAdding(Roles role)
    {
        if (role == null)
        {
            throw new BadRequestException("Invalid Role Model");
        }

        foreach (var permissions in role.Permissions)
        {
            var searchContext = new EntitiesPagingRequest<PermissionEntity>
            {
                Filter = x => x.Name == permissions.Name,
                PageNumber = 1,
                PerPage = int.MaxValue,
            };

            var permissionsEntity = await _unitOfWork.PermissionsRepository.SearchWithPagingAsync(searchContext);
            if (permissionsEntity.Items.Count < 1)
            {
                throw new BadRequestException("One or more specified permissios do not exist.");
            }
        }
    }

    private async Task ValidateRoleOnUpdating(Roles role)
    {
        if (role == null)
        {
            throw new BadRequestException("Invalid Role Model");
        }

        _ = await _unitOfWork.RolesRepository.GetByIdAsync(Guid.Parse(role.Id)) ??
            throw new BadRequestException("Role does not exist");

        foreach (var permission in role.Permissions)
        {
            var searchContext = new EntitiesPagingRequest<PermissionEntity>
            {
                Filter = x => x.Name == permission.Name,
                PageNumber = 1,
                PerPage = int.MaxValue,
            };

            var permissionsEntity = await _unitOfWork.PermissionsRepository.SearchWithPagingAsync(searchContext);
            if (permissionsEntity.Items.Count < 1)
            {
                throw new BadRequestException("One or more specified permissios do not exist.");
            }
        }
    }
    #endregion
}

