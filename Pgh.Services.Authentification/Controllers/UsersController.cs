using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgh.Auth.Dal.Repository.UnitOfWork;
using Pgh.Auth.Model.Models;
using Pgh.Auth.Model.ModelViews.Dto;
using Pgh.Common.Classes;
using Pgh.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pgh.Common.Enumeration;
using UsersDtoForRead = Pgh.Auth.Model.ModelViews.Dto.UsersDtoForRead;
using Microsoft.AspNetCore.Authorization;

namespace Pgh.Services.Authentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        //Need to added get list for navigation with parameter passed on by user(Filters)
        //Need to create the Read model for each users.
        private readonly IMapper _mapper;
        protected UriHelpers Help;
        protected UnitOfWork Work;


        public UsersController(IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }


        #region MyRegion

        ///  <summary>
        ///  Get list of users with parameter passed in the query.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Get api/Users/00000000-0000-0000-0000-000000000000
        ///     The Id must be a valid Guid.
        ///  </remarks>
        /// <returns>
        /// 1-The Users from the database if exist
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var obj = await Work.Users.Get(x => x.UsersId == id, 
                includes:source=>source.Include(a=>a.FkUsers));

            var res = _mapper.Map<UsersDtoForRead>(obj);
            return new JsonResult(res);
        }

        /// <summary>
        /// Get List Of Users With Pagination. 
        /// </summary>
        /// <param name="resourceParameters"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("List")]
        public async Task<ActionResult> List([FromQuery] ResourceParameters resourceParameters)
        {
            var list = await Work.Users.GetList(resourceParameters, new string[] { "Fk" });

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
            var personViews = _mapper.Map<IEnumerable<GroupeDtoReadUpdate>>(list);
            return new JsonResult(personViews);
        }

        
        ///  <summary>
        ///  Get list of users with recursive.(Tree list based on user ID).
        ///  </summary>
        ///  <remarks>
        ///  List of users with the specified parameter using the order defined in the database
        /// Return the urls for navigation to the next or previous collection in the header response.
        /// the page size by default is set to 1000
        ///  </remarks>
        /// <param name="userId"></param>
        ///  <response code="500">Internal server error</response>  
        [AllowAnonymous]
        [HttpGet("Get/{userId}")]
        public async Task<ActionResult> GetListHierarchy(Guid userId)
        {
            
            string temp = $"exec [dbo].[GetUsersTree] '{userId}'";
            var li = await Work.Users.ExecuteStoreQuery(temp);
           
            var personViews = _mapper.Map<IEnumerable<UsersDtoForRead>>(li);
            return new JsonResult(personViews);
        }

        #endregion


        #region Create Users

        ///  <summary>
        ///  Add a list of Users to the database from the body of the api call.(Passed by as an array)
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      post api/Users/
        ///
        /// Unlike the previous Post this one has a field UserSupGuid that you can put value in it if the user have a supervisor.
        ///  </remarks>
        /// <param name="li"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]UsersDtoCreateList li)
        {
            bool res = await CreateUsersHierarchy(li);

            if (res == false)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 500,
                    Message = "une Erreur c'est produit lors de l'ajout des utilisateurs."
                });
            }
            else
            {
                return Ok();
            }
            
        }

        [Authorize(Roles = "TotalControl")]
        private async Task<bool> CreateUsersHierarchy(UsersDtoCreateList li)
        {
            bool res = true;

            try
            {

                Users parentUsers = null;
                if (li.ParentId != Guid.Empty && li.ParentId != null)
                {
                    parentUsers = await Work.Users.Get(x => x.UsersId == li.ParentId);
                }

                Users user = new Users()
                {
                    FkUsers = parentUsers,
                    UsersId = Guid.NewGuid(),
                    UsersCode = li.LiUsers.UsersCode,
                    UsersLastName = li.LiUsers.UsersLastName,
                    UsersName = li.LiUsers.UsersName,
                    UsersBirthDate = li.LiUsers.UsersBirthDate,
                    UsersDateLeave = li.LiUsers.UsersDateLeave,
              
                    UsersGenderCode = li.LiUsers.UsersGenderCode,
                    UsersJoinDate = li.LiUsers.UsersJoinDate,
                    UsersMail = li.LiUsers.UsersMail,
                    UsersMailIntern = li.LiUsers.UsersMailIntern,
                    UsersPersonalNumber = li.LiUsers.UsersPersonalNumber,
                    UsersPhoneNumber = li.LiUsers.UsersPhoneNumber,
                    UsersPosteName = li.LiUsers.UsersPosteName,
                    UsersState = li.LiUsers.UsersState,

                };

                await Work.Users.Add(user);
                await Work.Complete();
                ///// Create the hierarchy

                if (li.LiUsers.SubUsers != null)
                {
                    foreach (var item in li.LiUsers.SubUsers)
                    {
                        await CreateSub(user, item);
                    }

                }
            }
            catch (Exception e)
            {
                res = false;
            }

            return res;
        }

        [Authorize(Roles = "TotalControl")]
        private async Task<bool> CreateSub(Users parent, UsersDtoForCreation li)
        {
            bool res = true;

            try
            {
                Users user = new Users()
                {
                    FkUsers = parent,
                    UsersId = Guid.NewGuid(),
                    UsersCode = li.UsersCode,
                    UsersLastName = li.UsersLastName,
                    UsersName = li.UsersName,
             
                    UsersBirthDate = li.UsersBirthDate,
                    UsersDateLeave = li.UsersDateLeave,
            
                    UsersGenderCode = li.UsersGenderCode,
                    UsersJoinDate = li.UsersJoinDate,
                    UsersMail = li.UsersMail,
                    UsersMailIntern = li.UsersMailIntern,
                    UsersPersonalNumber = li.UsersPersonalNumber,
                    UsersPhoneNumber = li.UsersPhoneNumber,
                    UsersPosteName = li.UsersPosteName,
                    UsersState = li.UsersState,

                };
                await Work.Users.Add(user);
                await Work.Complete();

                foreach (var item in li.SubUsers)
                {
                    await CreateSub(user, item);
                }
                
            }
            catch (Exception e)
            {
                res = false;
            }
            

            return res;
        }

        #endregion


        #region Update Users

        ///  <summary>
        ///  Update one or Many Users at the same time. Users list are passed as array.
        ///  </summary>
        ///  <remarks>
        ///      put api/Users/
        ///  </remarks>
        /// <param name="liEntity"></param>
        /// <returns>
        /// The Users Updated.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<UsersDtoForUpdate> liEntity)
        {
            foreach (var entity in liEntity)
            {
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "Veuillez ajouter des informations concernant utilisateur !"
                    });
                }

                var entityToUpdate = await Work.Users.Get(x => x.UsersCode == entity.UsersCode,
                    includes:source=>source.Include(a=>a.FkUsers));

                if (entityToUpdate == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "utilisateur n'existe pas dans la base de donneés veuillez verifier son ID !"
                    });
                }

                Users tempSup = null;
                if (entity.FkUsersId != Guid.Empty && entity.FkUsersId !=null)
                {
                    tempSup = await Work.Users.Get(x => x.UsersId == entity.FkUsersId);
                }
                
                _mapper.Map(entity, entityToUpdate);
                entityToUpdate.FkUsers = tempSup;

                await Work.Complete();
            }
            return Ok();
        }

        #endregion


        #region Delete Users

        ///  <summary>
        ///  Delete one user from the database.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/Users/
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
                    Message = "L'id de l'utilisateur est null !"
                });
            }

            var entity = await Work.Users.Get(x => x.UsersId == id);
            if (entity == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = "L'utilisateur n'existe pas dans la base de donneés!"
                });
            }
            
            var sub = await Work.Users.GetList(x => x.FkUsers.UsersId == id, 
                includes: source => source.Include(a => a.FkUsers));

            if (sub != null && sub.Any())
            {
                foreach (var variable in sub)
                {
                    variable.FkUsers = null;
                }
                await Work.Complete();
            }
            
            Work.Users.Remove(entity);
            await Work.Complete();
            return Ok();
        }

        ///  <summary>
        ///  Delete Many Users At the Same time.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      Delete api/Users/
        /// andp pass the list of Guid of the users you want to delete in the Body of the request.
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
                        Message = "L'id de l'utilisateur est null !"
                    });
                }

                var entity = await Work.Users.Get(x => x.UsersId == id);
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "L'utilisateur n'existe pas dans la base de donneés!"
                    });
                }
                
                var sub = await Work.Users.GetList(x => x.FkUsers.UsersId == id, 
                    includes: source => source.Include(a => a.FkUsers));

                if (sub != null && sub.Any())
                {
                    foreach (var variable in sub)
                    {
                        variable.FkUsers = null;
                    }
                    await Work.Complete();
                }


                Work.Users.Remove(entity);
                await Work.Complete();
            }

            return Ok();
        }

        #endregion
        
    }
}
