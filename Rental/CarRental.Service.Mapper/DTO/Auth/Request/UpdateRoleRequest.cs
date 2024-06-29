using CarRental.Service.Mapper.DTO.Auth.LoginRoleDTO.Role;

namespace CarRental.Service.Mapper.DTO.Auth.Request;

public class UpdateRoleRequest
{
    public UpdateRole Role { get; set; }

    public List<string> Permissions { get; set; }
}

