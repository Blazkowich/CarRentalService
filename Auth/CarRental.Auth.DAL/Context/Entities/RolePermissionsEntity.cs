namespace CarRental.Auth.DAL.Context.Entities;

public class RolePermissionsEntity
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public RolesEntity Role { get; set; }

    public PermissionEntity Permission { get; set; }
}

