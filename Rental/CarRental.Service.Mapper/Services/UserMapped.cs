using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.BLL.Services.Interfaces;
using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;
using CarRental.Service.Mapper.Services.Interfaces;

namespace CarRental.Service.Mapper.Services
{
    internal class UserMapped : IUserMapped
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserMapped(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<Guid> GetUserIdByNameAsync(string name)
        {
            var user = await _userService.GetUserByUserNameAsync(name);
            return Guid.Parse(user.Id);
        }

        public async Task<UserResponse> AddUserAsync(CreateUserRequest user)
        {
            var newUser = _mapper.Map<User>(user);
            var addedUser = await _userService.AddUserAsync(newUser);
            return _mapper.Map<UserResponse>(addedUser);
        }

        public Task DeleteUserAsync(Guid userId)
        {
            return _userService.DeleteUserAsync(userId);
        }

        public async Task<UserResponse> UpdateUserAsync(UpdateUserRequest user)
        {
            var updateUser = _mapper.Map<User>(user);
            var updatedUser = await _userService.UpdateUserAsync(updateUser);
            return _mapper.Map<UserResponse>(updatedUser);
        }

        public async Task<List<RolesResponse>> GetRolesByUserIdAsync(Guid id)
        {
            var roles = await _userService.GetRolesByUserIdAsync(id);
            return _mapper.Map<List<RolesResponse>>(roles);
        }
    }
}
