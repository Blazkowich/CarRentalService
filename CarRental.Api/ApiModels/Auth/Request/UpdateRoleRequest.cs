using CarRental.Api.ApiModels.Auth.HelperApiModels.RoleModels;

namespace CarRental.Api.ApiModels.Auth.Request;

public class UpdateRoleRequest
{
    public UpdateRole Role { get; set; }

    public List<string> Permissions { get; set; }
}

