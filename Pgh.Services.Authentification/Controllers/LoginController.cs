using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Pgh.Auth.Model.Models;
using Pgh.Common.Classes;
using Pgh.Common.Common;
using Pgh.Services.Authentification.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pgh.Auth.Dal.Repository.UnitOfWork;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Pgh.Services.Authentification.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //protected UriHelpers Help;
        protected UnitOfWork Work;

        public LoginController()
        {
            //Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Login([FromQuery]string userCode, [FromQuery]string appCode, [FromQuery]string userPassword)
        {
            var user = await Work.Users.Get(x => x.UsersCode == userCode);
            var application = await Work.Application.Get(x => x.AppCode == appCode);
            if (user == null)

                return BadRequest("L'utilisateur n'existe pas dans la base de données");

            if (application == null)

                return BadRequest("L'application n'existe pas dans la base de données");

            //    return new JsonResult(new ErrorDetails
            //{
            //    StatusCode = 404,
            //    Message = "L'utilisateur ou l'application n'existe pas dans la base de données"
            //});

            var pass = Crypt.EncryptCode(userPassword);


            var accessApp = await Work.AffApplicationUsers.Get(x => x.UsersId == user.UsersId
                              && x.AppId == application.AppId);

            if (accessApp == null)

                //return BadRequest($"L'utilisateur {userCode} n'a pas d'accès à l'application {appCode}");
                return BadRequest($"L'utilisateur {userCode} n'a pas d'accès à l'application {appCode}");

            var access = await Work.AffApplicationUsers.Get(x => x.UsersId == user.UsersId
                               && x.AppId == application.AppId && x.Password == pass);

            if (access == null)

                //return BadRequest($"L'utilisateur {userCode} n'a pas d'accès à l'application {appCode}");
                return BadRequest($"Mot de passe incorrect");
            //return new JsonResult(new ErrorDetails
            //{
            //    StatusCode = 404,
            //    Message = $"L'utilisateur {userCode} n'a pas d'accès à l'application {appCode}"
            //});


            //Get the list of the specific menus that have the same foreign key as the Application.
            var menus = await Work.Menu.GetList(x => x.FkApp.AppId == application.AppId,
                includes: source => source
                    .Include(e => e.AffRolesUsersMenus)
                    .ThenInclude(a => a.Role)
                    .ThenInclude(t => t.AffRolePermissions)
                    .ThenInclude(s => s.Permission)
                );

            menus = menus.Select(e => new Menus
            {
                AffRolesUsersMenus = e.AffRolesUsersMenus.Where(a => a.UsersId == user.UsersId).ToList(),
                FkMenu = e.FkMenu,
                FkApp = e.FkApp,
                MenuId = e.MenuId,
                AffRoleGroupMenus = e.AffRoleGroupMenus,
                MenuName = e.MenuName,
                MenuUrl = e.MenuUrl,
                MenuDescription = e.MenuDescription,
                MenuState = e.MenuState,
                MenuDisplayName = e.MenuDisplayName
            }).Where(a => a.AffRolesUsersMenus.Any());

            //Get list of the groupes that the current users have access and has association with the application.
            var groupes = await Work.Groupe.GetList(x => x.FkApp.AppId == application.AppId
                , includes: source =>
                    source.Include(a => a.AffGroupUsers)
                    .Include(z => z.AffRoleGroupMenus)
                    .ThenInclude(e => e.Role)
                    .ThenInclude(t => t.AffRolePermissions)
                    .ThenInclude(y => y.Permission)
                    );

            groupes = groupes.Select(e => new Groupes
            {
                AffGroupUsers = e.AffGroupUsers.Where(a => a.UsersId == user.UsersId && a.GrpId == e.GrpId).ToList(),
                FkApp = e.FkApp,
                AffRoleGroupMenus = e.AffRoleGroupMenus,
                GrpId = e.GrpId,
                GrpName = e.GrpName,
                GrpDescription = e.GrpDescription,
                GrpDisplayName = e.GrpDisplayName,
                GrpState = e.GrpState
            }).Where(a => a.AffGroupUsers.Any());

            //if (!groupes.Any() && !menus.Any())
            //{
            //    return BadRequest($"Veuillez Vérifier si l'utilisateur {userCode} appartient a un groupe où " + $"possède d'accès pour des menus spécifiques .");
            //    //return new JsonResult(new ErrorDetails
            //    //{
            //    //    StatusCode = 404,
            //    //    Message = $"Veuillez Vérifier si l'utilisateur {userCode} appartient a un groupe où " +
            //    //              $"possède d'accès pour des menus spécifiques ."
            //    //});
            //}

            return new JsonResult(CreateToken(groupes.ToList(), menus.ToList(), user, application));
        }

        [AllowAnonymous]
        private AuthModelDto CreateToken(List<Groupes> groupes, List<Menus> menus, Users user, Applications application)
        {
            AffRolesUsersMenus role11 = Work.AffRoleUsersMenus.Get(x => x.UsersId == user.UsersId).Result;

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("Poulina-Auth-Service"))
                .AddSubject(user.UsersName + " " + user.UsersLastName)
                .AddIssuer("Poulina.Security.Bearer")
                .AddAudience("Poulina.Security.Bearer")
                .AddClaim("MembershipId", user.UsersCode)
                .AddClaim(ClaimTypes.Role, Work.Role.Get(x => x.RoleId == role11.RoleId).Result.RoleName)
                //.AddExpiry(1)
                .AddExpiry(1200000)
                .Build();

            AuthModelDto model = new AuthModelDto
            {
                Token = token,
                Application = application.AppDisplayName,
                AppCode = application.AppCode,
                UserName = user.UsersLastName + " " + user.UsersName,
                UserFiliale = user.FilialeID.ToString(),
                UserMail = user.UsersMailIntern,
                PhoneInterne = user.UsersPhoneNumber,
                PhoneExterne = user.UsersPersonalNumber,
                RoleUser = Work.Role.Get(x => x.RoleId == role11.RoleId).Result.RoleDisplayName,
                UserLogin = user.UsersCode
            };

            List<MenuPermission> menuList = new List<MenuPermission>();

            foreach (var entity in groupes)
            {
                var roleGroupMenus = entity.AffRoleGroupMenus.Where(x => x.GrpId == entity.GrpId).ToList();

                foreach (var item in roleGroupMenus)
                {
                    MenuPermission menu = new MenuPermission
                    {
                        MenuId = item.MenuId,
                        MenuName = item.Menu.MenuName,
                        MenuUrl = item.Menu.MenuUrl,
                        MenuParentId = item.Menu.FkMenuId,
                        PermissionDetails = new List<PermissionDetail>()
                    };

                    List<Permissions> lipermissionList = item.Role.AffRolePermissions
                        .Select(a => a.Permission).ToList();

                    foreach (var perm in lipermissionList)
                    {
                        PermissionDetail permission = new PermissionDetail
                        {
                            PermissionId = perm.PermId,
                            PermissionName = perm.PermName,
                            GroupeId = entity.GrpId,
                            GroupeName = entity.GrpName,
                        };

                        if (menuList.Any(z => z.MenuId == menu.MenuId))
                        {
                            var x = menuList.FindIndex(a => a.MenuId == item.MenuId);
                            menuList[x].PermissionDetails.Add(permission);
                        }
                        else
                        {
                            menu.PermissionDetails.Add(permission);
                        }

                    }

                    if (menu.PermissionDetails.Any())
                    {
                        menuList.Add(menu);
                    }
                }
            }

            try
            {
                foreach (var item in menus)
                {
                    MenuPermission menu = new MenuPermission
                    {
                        MenuId = item.MenuId,
                        MenuName = item.MenuName,
                        MenuUrl = item.MenuUrl,
                        MenuParentId = item.FkMenuId,

                        PermissionDetails = new List<PermissionDetail>()
                    };

                    List<AffRolesUsersMenus> rolenames = item.AffRolesUsersMenus.ToList();

                    foreach (var rolesUsersMenu in rolenames)
                    {
                        List<Permissions> lipermissionList = rolesUsersMenu.Role.AffRolePermissions.Select(a => a.Permission).ToList();

                        foreach (var perm in lipermissionList)
                        {
                            PermissionDetail permission = new PermissionDetail
                            {
                                PermissionId = perm.PermId,
                                PermissionName = perm.PermName,
                                GroupeName = "Permission Unique"
                            };

                            if (menuList.Any(a => a.MenuId == item.MenuId))
                            {
                                var x = menuList.FindIndex(a => a.MenuId == item.MenuId);
                                menuList[x].PermissionDetails.Add(permission);
                            }
                            else
                            {
                                menu.PermissionDetails.Add(permission);
                            }
                        }
                    }

                    if (menu.PermissionDetails.Any())
                    {
                        menuList.Add(menu);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }


            model.MenuPermissions = menuList.Distinct().ToList();            
            return model;
        }

    }
}