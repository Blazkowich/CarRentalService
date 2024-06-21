using AutoMapper;
using CarRental.Auth.BLL.Models;
using CarRental.Auth.DAL.Context.Entities;
using System.Data;

namespace CarRental.Auth.BLL.AutoMapper;

public class AuthMapperBLL : Profile
{
    public AuthMapperBLL()
    {
        // User
        CreateMap<User, UserEntity>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));

        CreateMap<UserEntity, User>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(
                src => src.Roles.Select(usr => usr.Role)))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(
                src => src.Permissions.Select(usp => usp.Permission)));

        CreateMap<UserRolesEntity, Roles>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => src.RoleId));

        CreateMap<Roles, UserRolesEntity>()
            .ForMember(src => src.RoleId, opt => opt.MapFrom(src => src.Id));

        CreateMap<UserPermissionsEntity, Permissions>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => src.PermissionId));

        CreateMap<Permissions, UserPermissionsEntity>()
            .ForMember(src => src.PermissionId, opt => opt.MapFrom(src => src.Id));

        CreateMap<RefreshToken, UserEntity>()
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token))
            .ForMember(dest => dest.TokenCreated, opt => opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.TokenExpires, opt => opt.MapFrom(src => src.Expires));

        // Roles
        CreateMap<Roles, RolesEntity>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<RolesEntity, Roles>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name));

        // Permissions
        CreateMap<Permissions, PermissionEntity>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<PermissionEntity, Permissions>()
            .ForMember(src => src.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(src => src.Name, opt => opt.MapFrom(src => src.Name));
    }
}

