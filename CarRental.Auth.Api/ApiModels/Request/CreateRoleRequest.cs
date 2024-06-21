using CarRental.Auth.Api.ApiModels.HelperApiModels.RoleModels;

namespace CarRental.Auth.Api.ApiModels.Request;

public class CreateRoleRequest
{
    public CreateRole Role { get; set; }

    public List<string> Permissions { get; set; }
}

