using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgh.Auth.Dal.Repository.UnitOfWork;
using Pgh.Auth.Model.Models;
using Pgh.Auth.Model.ModelViews.Dto;
using Pgh.Common.Classes;
using Pgh.Common.Common;
using Pgh.Common.Enumeration;
using static System.Net.Mime.MediaTypeNames;
using Applications = Pgh.Auth.Model.Models.Applications;

namespace Pgh.Services.Authentification.Controllers
{
    [Route("api/Groupes")]
    [ApiController]
    public class GroupeController : ControllerBase
    {
        //Must first verify if the groupe Name is unique or not.
        private readonly IMapper _mapper;
        protected UriHelpers Help;
        protected UnitOfWork Work;

        public GroupeController(IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }

        #region Get Groupes
        
        ///  <summary>
        ///  Get list of Groupes with parameter passed in the query.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get api/Groupes/00000000-0000-0000-0000-000000000000
        ///     The Id must be a valid Guid.
        ///  </remarks>
        /// <returns>
        /// 1-The Groupes from the database if exist
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpGet("{grpId}")]
        public async Task<ActionResult> Get(Guid grpId)
        {
            var obj = await Work.Groupe.Get(x => x.GrpId == grpId, includes: source=>source.Include(x=>x.FkApp));
            var res = _mapper.Map<GroupeDtoReadUpdate>(obj);
            return new JsonResult(res);
        }
        
        ///  <summary>
        ///  Get list of Groupes with parameter passed in the query.(Not filtered)
        ///  </summary>
        ///  <remarks>
        ///  List of Groupes with the specified parameter using the order defined in the database
        /// Return the urls for navigation to the next or previous collection in the header response.
        /// the page size by default is set to 1000
        ///  </remarks>
        /// <param name="resourceParameters"></param>
        ///  <response code="500">Internal server error</response>  
        [HttpGet("GetList")]
        public async Task<ActionResult> GetList([FromQuery] ResourceParameters resourceParameters)
        {
            var list = await Work.Groupe.GetList(resourceParameters, new string[] { "FkApp" });

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
            var personViews = _mapper.Map<IEnumerable<GroupeDtoReadUpdate>>(list);
            return new JsonResult(personViews);
        }

        #endregion


        #region Create Groupes
        
        ///  <summary>
        ///  Add a list of Groupes to the database from the body of the api call.(Passed by as an array)
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      post api/Groupes/PostMany
        ///  </remarks>
        /// <param name="li"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]IEnumerable<GroupeDtoCreate> li)
        {
            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 500,
                    Message = "Veuillez ajouter une liste de Groupes!"
                });
            }

            var lidest = _mapper.Map<IEnumerable<Groupes>>(li);
            foreach (var entity in lidest)
            {
                var tempApplication = await Work.Application.Get(x => x.AppId == entity.FkAppId);
                var tmpGroupe = await Work.Groupe.Get(x => x.GrpName == entity.GrpName);
                
                if (tempApplication == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 500,
                        Message = $"Veuillez vérifier la liste des application affecter " +
                                  $"au différents groupes Groupes!"+
                                  $"L'application avec L'ID {entity.FkAppId} n'existe pas"+
                                  $"La transaction est annulé de la liste des groupes." +
                                  $"Merci de corriger ces problèmes."
                    });
                }

                if (tmpGroupe != null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 500,
                        Message = $"Un Groupe avec le même nom ' {entity.GrpName} ' " +
                                  $"existe déja dans la base de données!"+
                                  $"La transaction est annulé de la liste des groupes." +
                                  $"Merci de corriger ces problèmes."
                    });
                }
                
                entity.GrpId = Guid.NewGuid();
                entity.FkApp = tempApplication;
            }
            await Work.Groupe.AddRange(lidest);
            await Work.Complete();
            return new JsonResult(_mapper.Map<IEnumerable<GroupeDtoReadUpdate>>(lidest));
        }
        #endregion


        #region Update Groupess

        ///  <summary>
        ///  Update one or Many Groupes at the same time. Groupes list are passed as array.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      put api/Groupes/
        ///  </remarks>
        /// <param name="liEntity"></param>
        /// <returns>
        /// The Groupes Updated.(can be changed to return only the Ids of the Groupes , or a message with ok.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<GroupeDtoReadUpdate> liEntity)
        {

            foreach (var entity in liEntity)
            {
                var tempapp = await Work.Application.Get(x => x.AppId == entity.AppGuid);
                if (entity == null || tempapp==null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "Veuillez ajouter des informations concernant les Groupes !"
                    });
                }

                var entityToUpdate = await Work.Groupe.Get(x => x.GrpId == entity.GrpId);

                if (entityToUpdate == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = $"Le Groupe avec L'ID {entity.GrpId} n'existe pas dans la base de donneés!"+
                                  "La transaction est annulé de la liste des groupes." +
                                  "Merci de corriger ces problèmes."
                    });
                }
                
                _mapper.Map(entity, entityToUpdate);

                await Work.Complete();
            }


            return Ok();
        }

        #endregion


        #region Delete Groupess

        ///  <summary>
        ///  Delete one Groupes from the database.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/Groupes/
        ///  </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Message saying Ok or only status code 200.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 400,
                    Message = "L'id de la Groupes est null !"
                });
            }
            var entity = await Work.Groupe.Get(x => x.GrpId == id);
            if (entity == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = "La Groupes n'existe pas dans la base de donneés!"
                });
            }
            
            Work.Groupe.Remove(entity);
            await Work.Complete();
            return Ok();
        }


        ///  <summary>
        ///  Delete Many Groupes At the Same time.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/Groupes/
        /// and pass the list of Guid of the Groupes you want to delete in the Body of the request.
        ///  </remarks>
        /// <param name="liGuids"></param>
        /// <returns>
        /// Message saying Ok or only status code 200.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
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
                        Message = "L'id de la Groupes est null !"
                    });
                }

                var entity = await Work.Groupe.Get(x => x.GrpId == id);
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "La Groupes n'existe pas dans la base de donneés!"
                    });
                }
                
                Work.Groupe.Remove(entity);
                await Work.Complete();
            }

            return Ok();
        }

        #endregion


        #region Affectation Applications Users

        [HttpGet("/api/Groupes/{groupeId}/Users")]
        public async Task<ActionResult> GetGroupesUsers(Guid groupeId)
        {
            var obj = await Work.Groupe.Get(x => x.GrpId == groupeId, includes: source => source
                .Include(e => e.FkApp).Include(a => a.AffGroupUsers)
                .ThenInclude(z => z.Users).ThenInclude(w=>w.FkUsers));

            var res = _mapper.Map<GroupeUserDtoRead>(obj);

            return new JsonResult(res);

        }

        [HttpPost("/api/Groupes/{groupeId}/Users")]
        public async Task<ActionResult> AddUsersToGroupe(Guid groupeId, [FromBody]IEnumerable<Guid> liUsers)
        {
            if (!liUsers.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste des utilisateurs a affecter au groupe avec l'ID {groupeId} ."
                });
            }


            foreach (var user in liUsers)
            {
                var temp = await Work.AffGroupeUser.Get(x => x.UsersId == user && x.GrpId == groupeId);
                var tempUser = await Work.Users.Get(x => x.UsersId == user);
                if (temp == null && tempUser != null)
                {
                    AffGroupUsers aff = new AffGroupUsers
                    {
                        UsersId = user,
                        GrpId = groupeId
                        
                    };

                    await Work.AffGroupeUser.Add(aff);
                    await Work.Complete();
                }
            }


            var obj = await Work.Groupe.Get(x => x.GrpId == groupeId, includes: source => source
                .Include(e => e.FkApp).Include(a => a.AffGroupUsers)
                .ThenInclude(z => z.Users));

            var res = _mapper.Map<GroupeUserDtoRead>(obj);

            return new JsonResult(res);
        }
        
        [HttpDelete("/api/Groupes/{groupeId}/Users")]
        public async Task<ActionResult> RemoveGroupeUsers(Guid groupeId, [FromBody] IEnumerable<Guid> liGuids)
        {
            //Affect permission to a specific role
            if (!liGuids.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Veuillez ajouter une liste de utilisateur a supprimer du groupe avec ID {groupeId} ."
                });
            }

            var role = await Work.Groupe.Get(x => x.GrpId == groupeId);
            if (role == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = $"Le Groupe avec l'ID {groupeId} n'existe pas dans la base de données."
                });
            }

            foreach (var item in liGuids)
            {
                var temp = await Work.AffGroupeUser.Get(x => x.UsersId == item && x.GrpId== groupeId);
                if (temp != null)
                {
                    Work.AffGroupeUser.Remove(temp);
                    await Work.Complete();
                }

            }

            var obj = await Work.Groupe.Get(x => x.GrpId == groupeId, includes: source => source
                .Include(e => e.FkApp).Include(a => a.AffGroupUsers)
                .ThenInclude(z => z.Users));

            var res = _mapper.Map<GroupeUserDtoRead>(obj);

            return new JsonResult(res);

        }

        #endregion
        
    }
}