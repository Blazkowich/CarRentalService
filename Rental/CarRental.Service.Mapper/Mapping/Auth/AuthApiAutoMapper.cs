using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Service.Mapper.DTO.Auth.Request;
using CarRental.Service.Mapper.DTO.Auth.Response;
using System.Data;

namespace CarRental.Service.Mapper.Mapping.Auth;

public class AuthApiAutoMapper : Profile
{
    public AuthApiAutoMapper()
    {
        // Users
        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(roleId =>
            new Roles { Id = roleId.ToString() }).ToList()));

        CreateMap<UpdateUserRequest, User>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(roleId =>
           new Roles { Id = roleId.ToString() }).ToList()));

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<LoginUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Model.Login))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Model.Password));

        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        // Roles
        CreateMap<CreateRoleRequest, Roles>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(permissionName =>
            new Permissions { Name = permissionName }).ToList()));

        CreateMap<UpdateRoleRequest, Roles>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(permissionName =>
            new Permissions { Name = permissionName }).ToList()));

        CreateMap<Roles, RolesResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        // Permissions
        CreateMap<Permissions, PermissionsResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}

