using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Auth.DAL.Context.Entities;

public class UserPermissionsEntity
{
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }

    public Guid PermissionId { get; set; }

    public UserEntity User { get; set; }

    public PermissionEntity Permission { get; set; }
}

