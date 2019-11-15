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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace Pgh.Services.Authentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected UriHelpers Help;
        protected UnitOfWork Work;

        public MenusController(IMapper mapper, IUrlHelper urlHelper)
        {
            _mapper = mapper;
            Help = new UriHelpers(urlHelper);
            Work = new UnitOfWork(new AuthDbContext());
        }

        #region Get Menus

        ///  <summary>
        ///  Get list of Menu with parameter passed in the query.
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
            var obj = await Work.Menu.Get(x => x.MenuId == id, includes: source => source.Include(a => a.FkMenu));
            var res = _mapper.Map<MenusDtoRead>(obj);
            return new JsonResult(res);
        }

        [AllowAnonymous]
        [HttpGet("List")]
        public async Task<ActionResult> List([FromQuery]ResourceParameters resourceParameters)
        {
            var list = await Work.Menu.GetList(resourceParameters,includes: new string[] { "FkMenu" });

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
            var personViews = _mapper.Map<IEnumerable<MenusDtoRead>>(list);
            return new JsonResult(personViews);
        }

        ///  <summary>
        ///  Get list of Menus recursive.(Tree list based on Menus ID).
        ///  </summary>
        ///  <remarks>
        ///  List of Menus with the specified parameter using the order defined in the database
        ///  </remarks>
        /// <param name="menuId"></param>
        ///  <response code="500">Internal server error</response>  
        [AllowAnonymous]
        [HttpGet("Get/{menuId}")]
        public async Task<ActionResult> GetListHierarchy(Guid menuId)
        {

            string temp = $"exec [dbo].[GetMenusTree] '{menuId}'";
            var li = await Work.Menu.ExecuteStoreQuery(temp, new string[] { "FkMenu", "FkApp"});
                //includes:source=>source.Include(a=>a.Fk).Include(z=>z.Application));

            var views = _mapper.Map<IEnumerable<MenusDtoRead>>(li);
            return new JsonResult(views);
        }

        #endregion

        #region Create Menus

        ///  <summary>
        ///  Add a list of Menus to the database from the body of the api call.(Recursion Method)
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      post api/Menus/
        ///  </remarks>
        /// <param name="li"></param>
        /// <returns>
        /// The new User Created.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]MenusDtoCreateList li)
        {
            
           bool res =  await CreateMenusHierarchy(li);

            if (res == false)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 500,
                    Message = "une Erreur c'est produit lors de l'ajouts des menus."
                });
            }
            else
            {
                return Ok();
            }
            
        }

        [Authorize(Roles = "TotalControl")]
        private async Task<bool> CreateMenusHierarchy(MenusDtoCreateList li)
        {
            bool res = true;
            try
            {
                Menus parentMenu = null;
                if (li.ParentId != Guid.Empty && li.ParentId != null)
                {
                    parentMenu = await Work.Menu.Get(x => x.MenuId == li.ParentId);
                }

                Menus menu = new Menus()
                {
                    FkMenu = parentMenu,
                    MenuId = Guid.NewGuid(),
                    MenuName = li.LiMenu.MenuName,
                    MenuDisplayName = li.LiMenu.MenuDisplayName,
                    MenuUrl = li.LiMenu.MenuUrl,
                    MenuDescription = li.LiMenu.MenuDescription,
                    MenuState = li.LiMenu.MenuState,

                };

                await Work.Menu.Add(menu);
                await Work.Complete();
                ///// Create the hierarchy

                if (li.LiMenu.SubMenus != null)
                {
                    foreach (var item in li.LiMenu.SubMenus)
                    {
                        await CreateSub(menu, item);
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
        private async Task<bool> CreateSub(Menus parent, MenusDtoCreate li)
        {
            bool res = true;
            try
            {
                Menus menu = new Menus()
                {
                    FkMenu = parent,
                    FkAppId = li.ApplicationAppId,
                    MenuName = li.MenuName,
                    MenuId = Guid.NewGuid(),
                    MenuUrl = li.MenuUrl,
                    MenuDisplayName = li.MenuDisplayName,
                    MenuDescription = li.MenuDescription,

                };
                await Work.Menu.Add(menu);
                await Work.Complete();

                foreach (var item in li.SubMenus)
                {
                    await CreateSub(menu, item);
                }
            }
            catch (Exception e)
            {
                res = false;
            }

            return res;

           
            
        }

        #endregion

        #region Update Menus

        ///  <summary>
        ///  Update one or Many Menus at the same time. Menus list are passed as array.
        ///  </summary>
        ///  <remarks>
        ///  Sample request:
        ///      put api/Users/
        ///  </remarks>
        /// <param name="liEntity"></param>
        /// <returns>
        /// The Users Updated.(can be changed to return only the Ids of the Menus , or a message with ok.
        /// </returns>
        ///  <response code="500">Internal server error</response> 
        [Authorize(Roles = "TotalControl")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] IEnumerable<MenusDtoUpdate> liEntity)
        {

            foreach (var entity in liEntity)
            {
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 400,
                        Message = "Veuillez ajouter des informations concernant les menus!"
                    });
                }
                
                var entityToUpdate = await Work.Menu.Get(x => x.MenuId == entity.MenuId, includes: 
                    source => source.Include(a => a.FkMenu)
                    .Include(z=>z.FkApp));

                var app = await Work.Application.Get(x => x.AppId == entity.AppId);
                if (entityToUpdate == null || app==null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = $"Le Menu ' {entity.MenuId} ' ou l'application ' {entity.AppId} ' n'existe pas dans la base de donneés " +
                                  $"veuillez verifier les IDs !"
                    });
                }

                Menus tempSup = await Work.Menu.Get(x => x.MenuId == entity.MenuParentId);
                

                _mapper.Map(entity, entityToUpdate);
                entityToUpdate.FkMenu = tempSup;
                entityToUpdate.FkApp = app;
                await Work.Complete();
            }


            return Ok();
        }

        #endregion

        #region Delete Menus

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
                    Message = "L'id du Menu est null !"
                });
            }

            var entity = await Work.Menu.Get(x => x.MenuId == id, 
                includes: source => source.Include(a => a.FkMenu));
            if (entity == null)
            {
                return new JsonResult(new ErrorDetails
                {
                    StatusCode = 404,
                    Message = "Le Menu n'existe pas dans la base de donneés!"
                });
            }



            var sub = await Work.Menu.GetList(x => x.FkMenu.MenuId == id);

            Work.Menu.RemoveRange(sub);
            await Work.Complete();

            //if (sub != null && sub.Any())
            //{
            //    foreach (var variable in sub)
            //    {
            //        //we are not deleting all it's sub menu.
            //        variable.FkMenu = null;
            //    }
                
            //}
            
            Work.Menu.Remove(entity);
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
                        Message = "L'id du menu est null !"
                    });
                }

                var entity = await Work.Menu.Get(x => x.MenuId == id);
                if (entity == null)
                {
                    return new JsonResult(new ErrorDetails
                    {
                        StatusCode = 404,
                        Message = "Le Menu n'existe pas dans la base de donneés!"
                    });
                }
                
                var sub = await Work.Menu.GetList(x => x.FkMenu.MenuId == id, 
                    includes:source=>source.Include(a=>a.FkMenu));

                if (sub != null && sub.Any())
                {
                    foreach (var variable in sub)
                    {
                        variable.FkMenu = null;
                    }
                    await Work.Complete();
                }
                
                Work.Menu.Remove(entity);
                await Work.Complete();
            }
            return Ok();
        }

        #endregion


    }
}