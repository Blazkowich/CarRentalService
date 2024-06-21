namespace CarRental.Auth.DAL.Context.Entities;

public class PermissionEntity : BaseEntity
{
    public List<UserPermissionsEntity> UserPermissions { get; set; }

    public List<RolePermissionsEntity> RolePermissions { get; set; }
}

