using System.Linq;
using AutoMapper;
using Pgh.Auth.Model.Models;
using Pgh.Auth.Model.ModelViews.Dto;
using Pgh.Services.Authentification.Helpers;

namespace Pgh.Services.Authentification.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Users Entities Dto

            CreateMap<Users, UsersDtoForCreation>().ReverseMap();
            CreateMap<Users, UsersDtoForRead>()
                .ForMember(dest => dest.UsersFullName, opt => opt.MapFrom(src => src.UsersName.Trim() + " " + src.UsersLastName.Trim()))
                .ForMember(dest => dest.SupFullName, opt => opt.MapFrom(src => src.FkUsers.UsersName.Trim() + " " + src.FkUsers.UsersLastName.Trim()))
                .ForMember(dest => dest.UsersSupCode, opt => opt.MapFrom(src => src.FkUsers.UsersCode.Trim()))
                .ReverseMap();

            CreateMap<Users, UsersDtoForUpdate>().ReverseMap();

            #endregion


            #region Menu Entites dto mapping

            CreateMap<Menus, MenusDtoCreate>()
                .ForMember(dest => dest.ApplicationAppId, opt => opt.MapFrom(src => src.FkApp.AppId))
                .ReverseMap();
            CreateMap<Menus, MenusDtoRead>()
                .ForMember(dest => dest.MenuParentId, opt => opt.MapFrom(src => src.FkMenu.MenuId))
                .ForMember(dest => dest.MenuParentName, opt => opt.MapFrom(src => src.FkMenu.MenuName))
                .ForMember(dest => dest.AppId, opt => opt.MapFrom(src => src.FkAppId))
                .ForMember(dest => dest.AppName, opt => opt.MapFrom(src => src.FkApp.AppDisplayName))
                .ReverseMap();

            CreateMap<Menus, MenusDtoUpdate>()
                .ForMember(dest => dest.MenuParentId, opt => opt.MapFrom(src => src.FkMenu.MenuId))
                .ForMember(dest => dest.AppId, opt => opt.MapFrom(src => src.FkApp.AppId))
                .ReverseMap();

            #endregion

               
            CreateMap<Permissions, PermissionDtoCreate>().ReverseMap();
            CreateMap<Permissions, PermissionDtoReadUpdate>().ReverseMap();
            
            CreateMap<Roles, RoleDtoForCreate>().ReverseMap();
            CreateMap<Roles, RoleDtoForReadUpdate>().ReverseMap();

            CreateMap<Applications, ApplicationDtoCreate>().ReverseMap();
            CreateMap<Applications, ApplicationDtoRead>().ReverseMap();

            CreateMap<Groupes, GroupeDtoReadUpdate>()
                .ForMember(dest => dest.AppName, opt => opt.MapFrom(src => src.FkApp.AppName.Trim()))
                .ForMember(dest => dest.AppGuid, opt => opt.MapFrom(src => src.FkApp.AppId))
                .ReverseMap();

            CreateMap<Groupes, GroupeDtoCreate>().ReverseMap();

            CreateMap<Groupes, GroupeUserDtoRead>()
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src.AffGroupUsers.Select(x => x.Users).ToList()))
                .ForMember(dest => dest.AppName,
                    opt => opt.MapFrom(src => src.FkApp.AppName))
                .PreserveReferences()
                .ReverseMap().ForAllMembers(opt => opt.Ignore());

            
            //Association Tables Mapping
            CreateMap<Roles, RolePermissionDtoReadUpdate>()
                .ForMember(dest => dest.PermissionList,
                    opt => opt.MapFrom(src => src.AffRolePermissions.Select(x => x.Permission).ToList()))
                .PreserveReferences()
                .ForMember(c => c.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(c => c.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(c => c.RoleDescription, opt => opt.MapFrom(src => src.RoleDescription))
                .ForMember(c => c.RoleDisplayName, opt => opt.MapFrom(src => src.RoleDisplayName))
                .ForMember(c => c.RoleState, opt => opt.MapFrom(src => src.RoleState))
                .ReverseMap().ForAllMembers(opt => opt.Ignore());

            
            CreateMap<Users, ApplicationUsersDtoRead>().ReverseMap();

            CreateMap<Applications, ApplicationUsersDto>()
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src.AffApplicationUsers.Select(x => x.Users).ToList()))
                .AfterMap((model, entity) =>
                {
                    foreach (var entityUserAndTag in entity.Users)
                    {
                        var password = model.AffApplicationUsers
                            .Where(a => a.AppId == entity.AppId && a.UsersId == entityUserAndTag.UsersId)
                            .Select(z => z.Password).FirstOrDefault();
                        entityUserAndTag.Password = password;
                    }
                })
                .PreserveReferences()
                .ReverseMap().ForAllMembers(opt => opt.Ignore());


            CreateMap<AffRolesUsersMenus,RoleUsersMenusDto>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Menus,
                    opt => opt.MapFrom(src => src.Menu))
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.Users))
                .ReverseMap().ForAllMembers(opt => opt.Ignore());


            CreateMap<AffRoleGroupMenus, RoleGroupesMenusDto>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Menu,
                    opt => opt.MapFrom(src => src.Menu))
                .ForMember(dest => dest.Groupe,
                    opt => opt.MapFrom(src => src.Groupe))
                .ReverseMap().ForAllMembers(opt => opt.Ignore());
            
        }
    }
}