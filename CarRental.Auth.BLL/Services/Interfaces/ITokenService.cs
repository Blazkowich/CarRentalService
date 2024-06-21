using CarRental.Auth.BLL.Models;

namespace CarRental.Auth.BLL.Services.Interfaces;

public interface ITokenService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    Task<RefreshToken> CreateOrGenerateRefreshToken(User user, List<Roles> roles, List<Permissions> permissions);
}
