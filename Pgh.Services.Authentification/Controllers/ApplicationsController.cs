using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgh.Auth.Dal.Repository.UnitOfWork;
using Pgh.Auth.Model.Models;
using Pgh.Auth.Model.ModelViews.Dto;
using Pgh.Common.Classes;
using Pgh.Common.Common;
using Pgh.Common.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pgh.Services.Authentification.Controllers
{
    [Route("api/[controller]")]

    //[Route("api/Applications/{AppId}/Groupes")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected UriHelpers Help;
        protected UnitOfWork Work;

        public ApplicationsController(IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }

        #region Get Application
        
        ///  <summary>
        ///  Get list of Applications with parameter passed in the query.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get api/Application/00000000-0000-0000-0000-000000000000
        ///     The Id must be a valid Guid.
        ///  </remarks>
        /// <returns>
        /// 1-The Application from the database if exist
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpGet("{appId}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get(Guid appId)
        {
            var obj = await Work.Application.Get(x => x.AppId == appId);
            var res = _mapper.Map<ApplicationDtoRead>(obj);
            return new JsonResult(res);
        }
        
        ///  <summary>
        ///  Get list of Permission with parameter passed in the query.(Not filtered)
        ///  </summary>
        ///  <remarks>
        ///  List of permission with the specified parameter using the order defined in the database
        /// Return the urls for navigation to the next or previous collection in the header response.
        /// the page size by default is set to 1000
        ///  </remarks>
        /// <param name="resourceParameters"></param>
        ///  <response code="500">Internal server error</response>  
        [HttpGet("GetList")]
        [AllowAnonymous]
        public async Task<ActionResult> GetList([FromQuery] ResourceParameters resourceParameters)
        {
            var list = await Work.Application.GetList(resourceParameters);

            var previousPageLink = list.HasPrevious
                ? Help.CreateResourceUri(resourceParameters, ResourceUriType.PreviousPage, nameof(GetList))
                : null;

            var nextPageLink = list.HasNext
                ? Help.CreateResourceUri(resourceParameters, ResourceUriType.NextPage, nameof(GetList))
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
            var views = _mapper.Map<IEnumerable<ApplicationDtoRead>>(list);
            return new JsonResult(views);
        }

        #endregion
        
        #region Create Applications
        
        ///  <summary>
        ///  Add a list of application to the database from the body of the api call.(Passed by as an array)
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      post api/application/PostMany
        ///  </remarks>
        /// <param name="li"></param>
        /// <returns>
        /// The new Applications Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPost]
        [Authorize(Roles = "TotalControl")]
        public async Task<ActionResult> Post([FromBody]IEnumerable<ApplicationDtoCreate> li)
        {
            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 500,
                    Message = "Veuillez ajouter une liste d'applications !"
                });
            }

            var lidest = _mapper.Map<IEnumerable<Applications>>(li);

            foreach (var application in lidest)
            {
                var tempapp = await Work.Application.Get(x => x.AppCode == application.AppCode 
                                                        || x.AppName ==application.AppName);
                if (tempapp != null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = $"Une Application avec le nom ' {application.AppName} ' ou le code ' {application.AppCode} '" +
                                  $" existe déja dans la base de données." +
                                  $"La transaction est annulé de la liste des applications." +
                                  $"Merci de corriger ces problèmes."
                    });
                }
                application.AppId = Guid.NewGuid();
                
            }

            await Work.Application.AddRange(lidest);
            await Work.Complete();
            return new JsonResult(_mapper.Map<IEnumerable<ApplicationDtoRead>>(lidest));
            //return Ok();
        }
        #endregion
        
        #region Update Application

        ///  <summary>
        ///  Update one or Many Applications at the same time. Applications list are passed as array.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      put api/application/
        ///  </remarks>
        /// <param name="liEntity"></param>
        /// <returns>
        /// The applications Updated.(can be changed to return only the Ids of the applications , or a message with ok.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPut]
        [Authorize(Roles = "TotalControl")]
        public async Task<ActionResult> Put([FromBody] IEnumerable<ApplicationDtoRead> liEntity)
        {
            foreach (var entity in liEntity)
            {
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "Veuillez ajouter des informations concernant l'application !"
                    });
                }

                var entityToUpdate = await Work.Application.Get(x => x.AppId == entity.AppId);

                if (entityToUpdate == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = $"L'application avec l'ID {entity.AppId} n'existe pas dans la base de donneés veuillez verifier son Nom !"
                    });
                }
                //Check if there is an application with the same name or code before update.

                _mapper.Map(entity, entityToUpdate);

                await Work.Complete();
            }
            
            return Ok();
        }
        #endregion
        
        #region Delete applications

        ///  <summary>
        ///  Delete one applications from the database.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/applications/
        ///  </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Message saying Ok or only status code 200.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpDelete("{id}")]
        [Authorize(Roles = "TotalControl")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "L'id de l'application est null !"
                });
            }

            var entity = await Work.Application.Get(x => x.AppId == id, includes:source=>source.Include(a=>a.Menus));

            if (entity == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = "L'application n'existe pas dans la base de donneés!"
                });
            }
            
            Work.Application.Remove(entity);
            await Work.Complete();
            await DeleteMenusApplication();
            return Ok();
        }

        ///  <summary>
        ///  Delete Many applications At the Same time.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/application/
        ///  pass the list of Guid of the applications you want to delete in the Body of the request.
        ///  </remarks>
        /// <param name="liGuids"></param>
        /// <returns>
        /// Message saying Ok or only status code 200.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpDelete]
        [Authorize(Roles = "TotalControl")]
        public async Task<ActionResult> Delete([FromBody] List<Guid> liGuids)
        {
            foreach (var id in liGuids)
            {
                if (id == Guid.Empty)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "L'id de l'application ne peut pas être un Guid vide!"
                    });
                }

                var entity = await Work.Application.Get(x => x.AppId == id, includes: source => source.Include(a => a.Menus));
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = $"L'application avec l'ID {id} n'existe pas dans la base de donneés!"
                    });
                }
                
                Work.Application.Remove(entity);
                await Work.Complete();
                
            }
            await DeleteMenusApplication();
            return Ok();
        }

        #endregion
        
        #region Association Applications Users

        [HttpGet("/api/Applications/{applicationId}/Users")]
        public async Task<ActionResult> GetApplicationUsers(Guid applicationId)
        {
            var obj = await Work.Application.Get(x => x.AppId == applicationId, includes: source => source
                .Include(a => a.AffApplicationUsers)
                .ThenInclude(z => z.Users));
            
            var res = _mapper.Map<ApplicationUsersDto>(obj);

            return new JsonResult(res);

        }

        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPost("/api/Applications/{applicationId}/Users")]
        public async Task<ActionResult> AddUsersToApplication(Guid applicationId,[FromBody]IEnumerable<AppUsersDto> liUsers)
        {
            if (!liUsers.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste des utilisateurs a affecter a l'application {applicationId} ."
                });
            }
            
            foreach (var user in liUsers)
            {
                var temp = await Work.AffApplicationUsers.Get(x => x.UsersId == user.IdUser && x.AppId==applicationId);
                var tempUser = await Work.Users.Get(x => x.UsersId == user.IdUser);
                if (temp != null && tempUser != null)
                {
                    temp.Password = Crypt.EncryptCode(user.Password);
                    await Work.Complete();
                }
                else if (temp ==null && tempUser!=null)
                {
                    AffApplicationUsers aff = new AffApplicationUsers
                    {
                        AppId = applicationId,
                        UsersId = user.IdUser,
                        Password = Crypt.EncryptCode(user.Password)
                    };
                    await Work.AffApplicationUsers.Add(aff);
                    await Work.Complete();
                }
            }
            
            var obj = await Work.Application.Get(x => x.AppId == applicationId, includes: source => source
                .Include(a => a.AffApplicationUsers)
                .ThenInclude(z => z.Users));

            var res = _mapper.Map<ApplicationUsersDto>(obj);

            return new JsonResult(res);
        }

        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPut("/api/Applications/{applicationId}/Users")]
        public async Task<ActionResult> UpdateUsersToApplication(Guid applicationId, [FromBody]IEnumerable<AppUsersDto> liUsers)
        {
            if (!liUsers.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste des utilisateurs a affecter a l'application {applicationId} ."
                });
            }
            
            foreach (var user in liUsers)
            {
                var temp = await Work.AffApplicationUsers.Get(x => x.UsersId == user.IdUser);
                
                if (temp != null)
                {
                    temp.Password = Crypt.EncryptCode(user.Password);
                    await Work.Complete();
                }
            }

            var obj = await Work.Application.Get(x => x.AppId == applicationId, includes: source => source
                .Include(a => a.AffApplicationUsers)
                .ThenInclude(z => z.Users));

            var res = _mapper.Map<ApplicationUsersDto>(obj);
            return new JsonResult(res);
        }

        [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("/api/Applications/{applicationId}/Users")]
        public async Task<ActionResult> RemoveApplicationUsers(Guid applicationId, [FromBody] IEnumerable<Guid> liGuids)
        {
            //Affect permission to a specific role
            if (!liGuids.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste de utilisateur a supprimer de l'application role {applicationId} ."
                });
            }

            var role = await Work.Application.Get(x => x.AppId == applicationId);
            if (role == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"L'application avec l'id {applicationId} n'existe pas dans la base de données."
                });
            }

            foreach (var item in liGuids)
            {
                var temp = await Work.AffApplicationUsers.Get(x => x.UsersId == item && x.AppId == applicationId);
                if (temp != null)
                {
                    Work.AffApplicationUsers.Remove(temp);
                    await Work.Complete();
                }

            }

            var obj = await Work.Application.Get(x => x.AppId == applicationId, includes: source => source
                .Include(a => a.AffApplicationUsers)
                .ThenInclude(z => z.Users));

            var res = _mapper.Map<ApplicationUsersDto>(obj);

            return new JsonResult(res);

        }

        #endregion
        [Authorize(Roles = "TotalControl")]
        private async Task DeleteMenusApplication()
        {
            try
            {
                var menus = await Work.Menu.GetList(z => z.FkAppId == null);
                Work.Menu.RemoveRange(menus);
                await Work.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
    }
}