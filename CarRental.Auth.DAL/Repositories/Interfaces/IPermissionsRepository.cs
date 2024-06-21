﻿using CarRental.Auth.DAL.Context.Entities;
using CarRental.DAL.Common.Repositories.Interfaces;

namespace CarRental.Auth.DAL.Repositories.Interfaces;

public interface IPermissionsRepository : IBaseRepository<Guid, PermissionEntity>
{
    Task<List<PermissionEntity>> GetPermissionsByRoleIdAsync(Guid roleId);
}