using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Pgh.Auth.Dal.Repository.UnitOfWork;
using Pgh.Auth.Model.Models;
using Pgh.Auth.Model.ModelViews.Dto;
using Pgh.Common.Classes;
using Pgh.Common.Common;
using Pgh.Common.Enumeration;

namespace Pgh.Services.Authentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected UriHelpers Help;
        protected UnitOfWork Work;

        public RoleController(IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }

        #region Get Roles
        

        ///  <summary>
        ///  Get list of roles with parameter passed in the query.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get api/role/00000000-0000-0000-0000-000000000000
        ///     The Id must be a valid Guid.
        ///  </remarks>
        /// <returns>
        /// 1-The roles from the database if exist
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var obj = await Work.Role.Get(x => x.RoleId == id);
            var res = _mapper.Map<RoleDtoForReadUpdate>(obj);
            return new JsonResult(res);
        }


        ///  <summary>
        ///  Get list of Roles with parameter passed in the query.(Not filtered)
        ///  </summary>
        ///  <remarks>
        ///  List of role with the specified parameter using the order defined in the database
        /// Return the urls for navigation to the next or previous collection in the header response.
        /// the page size by default is set to 1000
        ///  </remarks>
        /// <param name="resourceParameters"></param>
        ///  <response code="500">Internal server error</response>  
        [AllowAnonymous]
        [HttpGet("List")]
        public async Task<ActionResult> List([FromQuery] ResourceParameters resourceParameters)
        {
            var list = await Work.Role.GetList(resourceParameters);

            var previousPageLink = list.HasPrevious
                ? Help.CreateResourceUri(resourceParameters, ResourceUriType.PreviousPage, nameof(List))
                : null;

            var nextPageLink = list.HasNext
                ? Help.CreateResourceUri(resourceParameters, ResourceUriType.NextPage, nameof(List))
                : null;

            var paginationMetadata = new
            {
                totalCount = list.TotalCount,
                pageSize = list.PageSize,
                currentPage = list.CurrentPage,
                totalPages = list.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert
                    .SerializeObject(paginationMetadata));
            var personViews = _mapper.Map<IEnumerable<RoleDtoForReadUpdate>>(list);
            return new JsonResult(personViews);
        }

        #endregion


        #region Create roles

        ///  <summary>
        ///  Add Permission to the database from the body of the api call/
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///  post api/role
        ///  </remarks>
        /// <param name="obj"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RoleDtoForCreate obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            var temp = await Work.Role.Get(x => x.RoleName == obj.RoleName);

            if (temp != null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 424,
                    Message = "Un Role avec le meme Nom exist dans la base de donneés"
                });
            }



            var entity = _mapper.Map<Roles>(obj);
            entity.RoleId = Guid.NewGuid();

            await Work.Role.Add(entity);
            await Work.Complete();

            return new JsonResult(_mapper.Map<RoleDtoForReadUpdate>(entity));
        }

        ///  <summary>
        ///  Add a list of roles to the database from the body of the api call.(Passed by as an array)
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      post api/role/PostMany
        ///  </remarks>
        /// <param name="li"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPost("AddList")]
        public async Task<ActionResult> AddList([FromBody]IEnumerable<RoleDtoForCreate> li)
        {
            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 500,
                    Message = "Veuillez ajouter une liste de roles!"
                });
            }

            var lidest = _mapper.Map<IEnumerable<Roles>>(li);

            await Work.Role.AddRange(lidest);
            await Work.Complete();
            return Ok();
        }
        #endregion


        #region Update roles

        ///  <summary>
        ///  Update one or Many roles at the same time. roles list are passed as array.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      put api/role/
        ///  </remarks>
        /// <param name="liEntity"></param>
        /// <returns>
        /// The Permission Updated.(can be changed to return only the Ids of the permissions , or a message with ok.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<RoleDtoForReadUpdate> liEntity)
        {

            foreach (var entity in liEntity)
            {
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "Veuillez ajouter des informations concernant le role !"
                    });
                }

                var entityToUpdate = await Work.Role.Get(x => x.RoleName == entity.RoleName);

                if (entityToUpdate == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "role n'existe pas dans la base de donneés veuillez verifier son Nom !"
                    });
                }


                _mapper.Map(entityToUpdate, entity);

                await Work.Complete();
            }


            return Ok();
        }
        #endregion


        #region Delete roles

        ///  <summary>
        ///  Delete one Permission from the database.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/role/
        ///  </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Message saying Ok or only status code 200.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            if (id == Guid.Empty)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "L'id du role est null !"
                });
            }

            var entity = await Work.Role.Get(x => x.RoleId == id);
            if (entity == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = "Le role n'existe pas dans la base de donneés!"
                });
            }




            Work.Role.Remove(entity);
            await Work.Complete();
            return Ok();
        }

        ///  <summary>
        ///  Delete Many roles At the Same time.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/role/
        /// pass the list of Guid of the roles you want to delete in the Body of the request.
        ///  </remarks>
        /// <param name="liGuids"></param>
        /// <returns>
        /// Message saying Ok or only status code 200.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] List<Guid> liGuids)
        {
            foreach (var id in liGuids)
            {
                if (id == Guid.Empty)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "L'id de la permission est null !"
                    });
                }

                var entity = await Work.Role.Get(x => x.RoleId == id);
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "Le role n'existe pas dans la base de donneés!"
                    });
                }
                
                Work.Role.Remove(entity);
                await Work.Complete();
            }

            return Ok();
        }

        #endregion



        #region Association Role Permission

        [HttpGet("/api/roles/{roleId}/permissions")]
        public async Task<ActionResult> GetRolePermissionlist(Guid roleId)
        {
            var obj = await Work.Role.GetList(x => x.RoleId == roleId, includes:source=>source.Include(x=>x.AffRolePermissions)
                .ThenInclude(a=>a.Permission));
            var res = _mapper.Map<IEnumerable<RolePermissionDtoReadUpdate>>(obj);
            return new JsonResult(res);
        }


        //we can change the following end point so that it can create the permission if it doees not exist and 
        // then affect to following roles.

        [HttpPost("/api/roles/{roleId}/permissions")]
        public async Task<ActionResult> AddRolePermissionlist(Guid roleId,[FromBody] IEnumerable<Guid> permList)
        {
            //Affect permission to a specific role
            if (!permList.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste de permission a affecter au role ' {roleId} '."
                });
            }
            
            var role = await Work.Role.Get(x => x.RoleId ==roleId);
            
            if (role == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Le role avec l'id {roleId} n'existe pas dans la base de données."
                });
            }
            
            List<AffRolePermissions> li = new List<AffRolePermissions>();
            
            foreach (var item in permList)
            {
                var tempPerm = await Work.Permission.Get(x => x.PermId == item);
                if (tempPerm == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = $"La permission avec l'ID ' {item} ' n'existe pas dans la base de données."
                    });
                }
                var temp = await Work.AffRolePermission.Get(x => x.PermId == item && x.RoleId == roleId);

                if (temp == null)
                {
                    AffRolePermissions aff = new AffRolePermissions
                    {
                        PermId = item,
                        RoleId = roleId
                    };
                    li.Add(aff);
                }
            }

            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Une erreur c'est produite lors de l'affectation des permissions au différents rôle!."
                });
            }

            await Work.AffRolePermission.AddRange(li);
            await Work.Complete();
            
            var obj = await Work.Role.GetList(x => x.RoleId == roleId, 
                includes: source => source.Include(x => x.AffRolePermissions)
                .ThenInclude(a => a.Permission));

            var res = _mapper.Map<IEnumerable<RolePermissionDtoReadUpdate>>(obj);

            return new JsonResult(res);
        }



        [HttpDelete("/api/roles/{roleId}/permissions")]
        public async Task<ActionResult> RemoveRolePermissionlistGuid(Guid roleId, [FromBody] IEnumerable<Guid> liGuids)
        {
            //Affect permission to a specific role
            if (!liGuids.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste de permission a supprimer au role {roleId} ."
                });
            }

            var role = await Work.Role.Get(x => x.RoleId == roleId);
            if (role == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Le role avec l'id {roleId} n'existe pas dans la base de données."
                });
            }
            
            foreach (var item in liGuids)
            {
                var temp = await Work.AffRolePermission.Get(x => x.PermId == item && x.RoleId == roleId);
                if (temp !=null)
                {
                    Work.AffRolePermission.Remove(temp);
                    await Work.Complete();
                }

            }

            var obj = await Work.Role.GetList(x => x.RoleId == roleId, includes: source => source.Include(x => x.AffRolePermissions)
                .ThenInclude(a => a.Permission));
            var res = _mapper.Map<IEnumerable<RolePermissionDtoReadUpdate>>(obj);
            return new JsonResult(res);

        }
        
        //The Update permission or handle the delete of roles 
        private async Task<IEnumerable<PermissionDtoReadUpdate>> GetPermissionList(IEnumerable<PermissionDtoCreate> lipermission)
        {
            List<PermissionDtoReadUpdate> li = new List<PermissionDtoReadUpdate>();
            foreach (var item in lipermission)
            {
                var temp = await Work.Permission.Get(x => x.PermName == item.PermName);

                if (temp == null)
                {
                    //if Permission does not exist add it to the permission database.
                    //Permission Name column is unique.
                    var obj = _mapper.Map<Permissions>(item);
                    obj.PermId = Guid.NewGuid();
                    await Work.Permission.Add(obj);
                    await Work.Complete();
                    li.Add(_mapper.Map<PermissionDtoReadUpdate>(obj));

                }
                else
                {
                    li.Add(_mapper.Map<PermissionDtoReadUpdate>(temp));
                }
            }

            return li;
        }
        #endregion

        #region Association Role Users Menus
        [AllowAnonymous]
        [HttpGet("/api/roles/Users-Menus")]
        public async Task<ActionResult> GetUsersMenusList([FromQuery] RoleUsersMenusDtoGetDelete query)
        {
            IEnumerable<AffRolesUsersMenus> obj;

            
            var result =  typeof(RoleUsersMenusDtoGetDelete).GetProperties()
                .Select(x => new { property = x.Name, value = x.GetValue(query) })
                .Where(x => x.value != null)
                .ToList();

            var predicate = PredicateBuilder.True<AffRolesUsersMenus>();
            
            if (result.Count == 1)
            {
                string temp = result[0].property;
                Guid guid = new Guid(result[0].value.ToString());
                if (temp == "UserId" && guid!=Guid.Empty)
                {
                    predicate = predicate.And(p => p.UsersId == guid);
                }
                else if (temp == "MenuId" && guid != Guid.Empty)
                {
                    predicate = predicate.And(p => p.MenuId == guid);
                }
                else if (temp == "RoleId" && guid != Guid.Empty)
                {
                    predicate = predicate.And(p => p.RoleId == guid);
                }
            }
            else if(result.Count > 1)
            {
                foreach (var keyword in result)
                {
                    string temp = keyword.property;
                    
                    Guid guid = new Guid(keyword.value.ToString());
                    if (temp == "UserId" && guid != Guid.Empty)
                    {
                        predicate = predicate.And(p => p.UsersId == guid);
                    }
                    else if (temp == "MenuId" && guid != Guid.Empty)
                    {
                        predicate = predicate.And(p => p.MenuId == guid);
                    }
                    else if (temp == "RoleId" && guid != Guid.Empty)
                    {
                        predicate = predicate.And(p => p.RoleId == guid);
                    }
                }
            }
            else
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez un paramètre au choix pour votre recherche."
                });
            }

            obj = await Work.AffRoleUsersMenus.GetList(predicate,
                includes: source => source.Include(x => x.Role)
                    .Include(a => a.Menu).ThenInclude(q => q.FkMenu)
                    .Include(a => a.Users).ThenInclude(z => z.FkUsers));
            var res = _mapper.Map<IEnumerable<RoleUsersMenusDto>>(obj);

            return new JsonResult(res);
        }

        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPost("/api/roles/Users-Menus")]
        public async Task<ActionResult> PostUsersMenusList([FromBody]IEnumerable<RoleUsersMenusDtoCreate> li)
        {
            List<AffRolesUsersMenus> obj = new List<AffRolesUsersMenus>();
            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste des utilisateurs et de permission a affecter au différents role  ."
                });
            }

            foreach (var item in li)
            {
                var role = await Work.Role.Get(x => x.RoleId == item.RoleId);
                var menu = await Work.Menu.Get(x => x.MenuId == item.MenuId);
                var user = await Work.Users.Get(x => x.UsersId == item.UserId);
                if (role != null && menu !=null && user !=null)
                {
                    try
                    {
                        AffRolesUsersMenus aff = new AffRolesUsersMenus
                        {
                            UsersId = user.UsersId,
                            MenuId = menu.MenuId,
                            RoleId = role.RoleId
                        };
                        await Work.AffRoleUsersMenus.Add(aff);
                        await Work.Complete();
                        obj.Add(aff);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            
            var res = _mapper.Map<IEnumerable<RoleUsersMenusDto>>(obj);
            return new JsonResult(res);
        }

        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPut("/api/roles/Users-Menus")]
        public async Task<ActionResult> PutRolesUsersMenus([FromBody] RoleUsersMenusDtoUpdate liEntity ,Guid Id)
        {
            
                RoleUsersMenusDtoGetDelete aff = new RoleUsersMenusDtoGetDelete
                {
                    UserId = liEntity.UserId,
                    MenuId = liEntity.MenuId,
                    RoleId = Id
                };

                await DeleteUsersMenusList(aff);



                var role = await Work.Role.Get(x => x.RoleId == liEntity.RoleId);
                var menu = await Work.Menu.Get(x => x.MenuId == liEntity.MenuId);
                var user = await Work.Users.Get(x => x.UsersId == liEntity.UserId);
                if (role != null && menu != null && user != null)
                {
                    AffRolesUsersMenus aff1 = new AffRolesUsersMenus
                    {
                        UsersId = user.UsersId,
                        MenuId = menu.MenuId,
                        RoleId = role.RoleId
                    };

                    
                    await Work.AffRoleUsersMenus.Add(aff1);
                   

                }
                
           
            await Work.Complete();

            return Ok();
        }
        #endregion


        [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("/api/roles/Users-Menus")]
        public async Task<ActionResult> DeleteUsersMenusList([FromQuery] RoleUsersMenusDtoGetDelete query)
        {


            var result = typeof(RoleUsersMenusDtoGetDelete).GetProperties()
                 .Select(x => new { property = x.Name, value = x.GetValue(query) })
                 .Where(x => x.value != null)
                 .ToList();

            var predicate = PredicateBuilder.True<AffRolesUsersMenus>();

            if (result.Count == 1)
            {
                string temp = result[0].property;
                Guid guid = new Guid(result[0].value.ToString());
                if (temp == "UserId" && guid != Guid.Empty)
                {
                    predicate = predicate.And(p => p.UsersId == guid);
                }
                else if (temp == "MenuId" && guid != Guid.Empty)
                {
                    predicate = predicate.And(p => p.MenuId == guid);
                }
                else if (temp == "RoleId" && guid != Guid.Empty)
                {
                    predicate = predicate.And(p => p.RoleId == guid);
                }
            }
            else if (result.Count > 1)
            {
                foreach (var keyword in result)
                {
                    string temp = keyword.property;

                    Guid guid = new Guid(keyword.value.ToString());
                    if (temp == "UserId" && guid != Guid.Empty)
                    {
                        predicate = predicate.And(p => p.UsersId == guid);
                    }
                    else if (temp == "MenuId" && guid != Guid.Empty)
                    {
                        predicate = predicate.And(p => p.MenuId == guid);
                    }
                    else if (temp == "RoleId" && guid != Guid.Empty)
                    {
                        predicate = predicate.And(p => p.RoleId == guid);
                    }
                }
            }
            else
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez entrez un paramétre aux choix pour votre requête."
                });
            }
            
            var entitiesToDelete = await Work.AffRoleUsersMenus.GetList(predicate,
                includes: source => source.Include(x => x.Role)
                    .Include(a => a.Menu).ThenInclude(q => q.FkMenu)
                    .Include(a => a.Users).ThenInclude(z => z.FkUsers));


            if (entitiesToDelete == null) return Ok();
            
            Work.AffRoleUsersMenus.RemoveRange(entitiesToDelete);
            await Work.Complete();

            return Ok();
        }

       



        #region Association Role Groupes Menus
        [AllowAnonymous]
        [HttpGet("/api/roles/Groupes-Menus")]
        public async Task<ActionResult> GetGroupesMenusList([FromQuery] RoleGroupesMenusDtoGetDelete query)
        {
            IEnumerable<AffRoleGroupMenus> obj;


            var result = typeof(RoleGroupesMenusDtoGetDelete).GetProperties()
                .Select(x => new { property = x.Name, value = x.GetValue(query) })
                .Where(x => x.value != null)
                .ToList();

            var predicate = PredicateBuilder.True<AffRoleGroupMenus>();

            if (result.Count == 1)
            {
                var temp = result[0].property;
                var guid = new Guid(result[0].value.ToString());
                switch (temp)
                {
                    case "GroupeId" when guid != Guid.Empty:
                        predicate = predicate.And(p => p.GrpId == guid);
                        break;
                    case "MenuId" when guid != Guid.Empty:
                        predicate = predicate.And(p => p.MenuId == guid);
                        break;
                    case "RoleId" when guid != Guid.Empty:
                        predicate = predicate.And(p => p.RoleId == guid);
                        break;
                }
            }
            else if (result.Count > 1)
            {
                foreach (var keyword in result)
                {
                    var temp = keyword.property;

                    var guid = new Guid(keyword.value.ToString());
                    switch (temp)
                    {
                        case "GroupeId" when guid != Guid.Empty:
                            predicate = predicate.And(p => p.GrpId == guid);
                            break;
                        case "MenuId" when guid != Guid.Empty:
                            predicate = predicate.And(p => p.MenuId == guid);
                            break;
                        case "RoleId" when guid != Guid.Empty:
                            predicate = predicate.And(p => p.RoleId == guid);
                            break;
                    }
                }
            }
            else
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez un paramètre au choix pour votre recherche."
                });
            }



            obj = await Work.AffRoleGroupMenu.GetList(predicate,
                includes: source => source.Include(x => x.Role)
                    .Include(a => a.Menu).ThenInclude(q => q.FkMenu)
                    .Include(e => e.Groupe));
            var res = _mapper.Map<IEnumerable<RoleGroupesMenusDto>>(obj);

            return new JsonResult(res);
        }

        [Authorize(Roles = "TotalControl")]
        [HttpPost("/api/roles/Groupes-Menus")]
        public async Task<ActionResult> PostGroupesMenusList([FromBody]IEnumerable<RoleGroupesMenusDtoCreate> li)
        {
            var obj = new List<AffRoleGroupMenus>();
            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste des utilisateurs et de permission a affecter au différents role  ."
                });
            }

            foreach (var item in li)
            {
                var role = await Work.Role.Get(x => x.RoleId == item.RoleId);
                var menu = await Work.Menu.Get(x => x.MenuId == item.MenuId);
                var groupe = await Work.Groupe.Get(x => x.GrpId == item.GroupeId);
                if (role != null && menu != null && groupe != null)
                {
                    try
                    {
                        var aff = new AffRoleGroupMenus
                        {
                            GrpId = groupe.GrpId,
                            MenuId = menu.MenuId,
                            RoleId = role.RoleId
                        };
                        await Work.AffRoleGroupMenu.Add(aff);
                        await Work.Complete();
                        obj.Add(aff);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }


            var res = _mapper.Map<IEnumerable<RoleGroupesMenusDto>>(obj);
            return new JsonResult(res);
        }

        [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("/api/roles/Groupes-Menus")]
        public async Task<ActionResult> DeleteGroupesMenusList([FromQuery] RoleGroupesMenusDtoGetDelete query)
        {


            var result = typeof(RoleGroupesMenusDtoGetDelete).GetProperties()
                 .Select(x => new { property = x.Name, value = x.GetValue(query) })
                 .Where(x => x.value != null)
                 .ToList();

            var predicate = PredicateBuilder.True<AffRoleGroupMenus>();

            if (result.Count == 1)
            {
                var temp = result[0].property;
                var guid = new Guid(result[0].value.ToString());
                switch (temp)
                {
                    case "GroupeId" when guid != Guid.Empty:
                        predicate = predicate.And(p => p.GrpId == guid);
                        break;
                    case "MenuId" when guid != Guid.Empty:
                        predicate = predicate.And(p => p.MenuId == guid);
                        break;
                    case "RoleId" when guid != Guid.Empty:
                        predicate = predicate.And(p => p.RoleId == guid);
                        break;
                }
            }
            else if (result.Count > 1)
            {
                foreach (var keyword in result)
                {
                    var temp = keyword.property;

                    var guid = new Guid(keyword.value.ToString());
                    switch (temp)
                    {
                        case "GroupeId" when guid != Guid.Empty:
                            predicate = predicate.And(p => p.GrpId == guid);
                            break;
                        case "MenuId" when guid != Guid.Empty:
                            predicate = predicate.And(p => p.MenuId == guid);
                            break;
                        case "RoleId" when guid != Guid.Empty:
                            predicate = predicate.And(p => p.RoleId == guid);
                            break;
                    }
                }
            }
            else
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez entrez un paramétre aux choix pour votre requête."
                });
            }

            var entitiesToDelete = await Work.AffRoleGroupMenu.GetList(predicate,
                includes: source => source.Include(x => x.Role)
                    .Include(a => a.Menu).ThenInclude(q => q.FkMenu)
                    .Include(a => a.Groupe));


            if (entitiesToDelete == null) return Ok();

            Work.AffRoleGroupMenu.RemoveRange(entitiesToDelete);
            await Work.Complete();

            return Ok();
        }

        #endregion
    }
}