using CarRental.Auth.Api.ApiModels.HelperApiModels.RoleModels;

namespace CarRental.Auth.Api.ApiModels.Request;

public class UpdateRoleRequest
{
    public UpdateRole Role { get; set; }

    public List<string> Permissions { get; set; }
}

