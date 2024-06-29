using CarRental.Service.Mapper.DTO.Auth.LoginRoleDTO.User;

namespace CarRental.Service.Mapper.DTO.Auth.Request;

public class UpdateUserRequest
{
    public UpdateUser User { get; set; }

    public List<Guid> Roles { get; set; }

    public string Password { get; set; }
}
