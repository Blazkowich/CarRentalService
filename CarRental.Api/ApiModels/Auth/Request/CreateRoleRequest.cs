using CarRental.Api.ApiModels.Auth.HelperApiModels.RoleModels;

namespace CarRental.Api.ApiModels.Auth.Request;

public class CreateRoleRequest
{
    public CreateRole Role { get; set; }

    public List<string> Permissions { get; set; }
}

