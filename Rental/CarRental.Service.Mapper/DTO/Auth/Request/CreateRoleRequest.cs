using CarRental.Service.Mapper.DTO.Auth.LoginRoleDTO.Role;

namespace CarRental.Service.Mapper.DTO.Auth.Request;

public class CreateRoleRequest
{
    public CreateRole Role { get; set; }

    public List<string> Permissions { get; set; }
}

