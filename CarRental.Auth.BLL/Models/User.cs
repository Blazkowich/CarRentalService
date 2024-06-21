namespace CarRental.Auth.BLL.Models;

public class User : BaseModel
{
    public string Password { get; set; }

    public List<Roles> Roles { get; set; }

    public List<Permissions> Permissions { get; set; }
}

