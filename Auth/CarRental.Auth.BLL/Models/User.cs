namespace CarRental.Auth.BLL.Models;

public class User : BaseModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Password { get; set; }

    public List<Roles> Roles { get; set; }

    public List<Permissions> Permissions { get; set; }
}

