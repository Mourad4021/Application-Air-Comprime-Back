using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class PermissionsController : ControllerBase
    {

        private readonly IMapper _mapper;
        protected UriHelpers Help;
        protected UnitOfWork Work;

        public PermissionsController(IMapper mapper,IUrlHelper urlHelper)
        {
            _mapper = mapper;
            Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }

        #region GetPermission
        

        ///  <summary>
        ///  Get list of Permissions with parameter passed in the query.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get api/Permissions/00000000-0000-0000-0000-000000000000
        ///     The Id must be a valid Guid.
        ///  </remarks>
        /// <returns>
        /// 1-The Permissions from the database if exist
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var obj = await Work.Permission.Get(x => x.PermId == id);
            var res = _mapper.Map<PermissionDtoReadUpdate>(obj);
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
        public async Task<ActionResult> GetList([FromQuery] ResourceParameters resourceParameters)
        {
            var list = await Work.Permission.GetList(resourceParameters);

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
            var personViews = _mapper.Map<IEnumerable<PermissionDtoReadUpdate>>(list);
            return new JsonResult(personViews);
        }

        #endregion


        #region Create Permission

        ///  <summary>
        ///  Add Permission to the database from the body of the api call/
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///  post api/Permissions
        ///  </remarks>
        /// <param name="permission"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PermissionDtoCreate permission)
        {
            if (permission == null)
            {
                return BadRequest();
            }

            var temp = await Work.Permission.Get(x => x.PermName == permission.PermName);

            if (temp != null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 424,
                    Message = "Une permission avec le meme Nom exist dans la base de donneés"
                });
            }

            

            var entity = _mapper.Map<Permissions>(permission);
            entity.PermId = Guid.NewGuid();
            
            await Work.Permission.Add(entity);
            await Work.Complete();

            return new JsonResult(_mapper.Map<PermissionDtoReadUpdate>(entity));
        }

        ///  <summary>
        ///  Add a list of Permissions to the database from the body of the api call.(Passed by as an array)
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      post api/Permissions/PostMany
        ///  </remarks>
        /// <param name="li"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPost("PostMany")]
        public async Task<ActionResult> PostMany([FromBody]IEnumerable<PermissionDtoCreate> li)
        {
            if (!li.Any())
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 500,
                    Message = "Veuillez ajouter une liste de permissions!"
                });
            }

            var lidest = _mapper.Map<IEnumerable<Permissions>>(li);
            
            await Work.Permission.AddRange(lidest);
            await Work.Complete();
            return Ok();
        }
        #endregion


        #region Update Permissions

        ///  <summary>
        ///  Update one or Many Permission at the same time. Permissions list are passed as array.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      put api/Permissions/
        ///  </remarks>
        /// <param name="liEntity"></param>
        /// <returns>
        /// The Permission Updated.(can be changed to return only the Ids of the permissions , or a message with ok.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<PermissionDtoReadUpdate> liEntity)
        {

            foreach (var entity in liEntity)
            {
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "Veuillez ajouter des informations concernant la permission !"
                    });
                }

                var entityToUpdate = await Work.Permission.Get(x => x.PermId == entity.PermId);

                if (entityToUpdate == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "Permission n'existe pas dans la base de donneés veuillez verifier son Nom !"
                    });
                }
                

                _mapper.Map(entity, entityToUpdate);
                
                await Work.Complete();
            }


            return Ok();
        }
        #endregion


        #region Delete Permissions

        ///  <summary>
        ///  Delete one Permission from the database.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/Permissions/
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
                    Message = "L'id de la permission est null !"
                });
            }

            var entity = await Work.Permission.Get(x => x.PermId == id);
            if (entity == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = "La permission n'existe pas dans la base de donneés!"
                });
            }

            


            Work.Permission.Remove(entity);
            await Work.Complete();
            return Ok();
        }

        ///  <summary>
        ///  Delete Many Permissions At the Same time.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/Permissions/
        /// andp pass the list of Guid of the Permissions you want to delete in the Body of the request.
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
                        Message = "L'id de la permission est null !"
                    });
                }

                var entity = await Work.Permission.Get(x => x.PermId == id);
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "La permission n'existe pas dans la base de donneés!"
                    });
                }

                


                Work.Permission.Remove(entity);
                await Work.Complete();
            }

            return Ok();
        }

        #endregion

    }
}