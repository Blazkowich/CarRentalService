using CarRental.Api.ApiModels.Auth.HelperApiModels.UserModels;

namespace CarRental.Api.ApiModels.Auth.Request;

public class UpdateUserRequest
{
    public UpdateUser User { get; set; }

    public List<Guid> Roles { get; set; }

    public string Password { get; set; }
}
