namespace CarRental.Auth.DAL.Context.Entities;

public class UserEntity : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime TokenCreated { get; set; } = DateTime.UtcNow;

    public DateTime TokenExpires { get; set; }

    public List<UserRolesEntity> Roles { get; set; } = [];

    public List<UserPermissionsEntity> Permissions { get; set; } = [];
}

