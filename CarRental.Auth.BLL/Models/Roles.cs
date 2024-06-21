namespace CarRental.Auth.BLL.Models;

public class Roles : BaseModel
{
    public List<Permissions> Permissions { get; set; }
}


