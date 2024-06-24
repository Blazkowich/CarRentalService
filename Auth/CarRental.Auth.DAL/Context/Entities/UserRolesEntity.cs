using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Auth.DAL.Context.Entities;

public class UserRolesEntity
{
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public UserEntity User { get; set; }

    public RolesEntity Role { get; set; }
}

