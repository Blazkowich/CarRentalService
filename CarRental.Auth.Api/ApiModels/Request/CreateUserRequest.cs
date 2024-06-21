using CarRental.Auth.Api.ApiModels.HelperApiModels.UserModels;

namespace CarRental.Auth.Api.ApiModels.Request;

public class CreateUserRequest
{
    public CreateUser User { get; set; }

    public List<Guid> Roles { get; set; }

    public string Password { get; set; }
}

