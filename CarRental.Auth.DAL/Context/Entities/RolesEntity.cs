namespace CarRental.Auth.DAL.Context.Entities;

public class RolesEntity : BaseEntity
{
    public List<UserRolesEntity> UserRoles { get; set; }

    public List<RolePermissionsEntity> RolePermissions { get; set; }
}


