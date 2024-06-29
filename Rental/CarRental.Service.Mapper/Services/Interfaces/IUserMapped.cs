using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;

namespace CarRental.Service.Mapper.Services.Interfaces
{
    public interface IUserMapped
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(Guid id);
        Task<Guid> GetUserIdByNameAsync(string name);
        Task<UserResponse> AddUserAsync(CreateUserRequest user);
        Task DeleteUserAsync(Guid userId);
        Task<UserResponse> UpdateUserAsync(UpdateUserRequest user);
        Task<List<RolesResponse>> GetRolesByUserIdAsync(Guid id);
    }
}
