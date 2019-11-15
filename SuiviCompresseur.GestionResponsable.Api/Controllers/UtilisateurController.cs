using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SuiviCompresseur.Gestion.Responsable.Aplication.Interfaces;
using SuiviCompresseur.Gestion.Responsable.Data.Context;
using SuiviCompresseur.Gestion.Responsable.Domain.Commands;
using SuiviCompresseur.Gestion.Responsable.Domain.Models;
using SuiviCompresseur.Gestion.Responsable.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SuiviCompresseur.GestionResponsable.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UsersController : ControllerBase
    {
        private readonly Gestion_Responsable_DBContext _context;
        private readonly IMediator mediator;

        public UsersController(Gestion_Responsable_DBContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
        }

        // GET api/Users
        //[AllowAnonymous]
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpGet]
        public async Task<IEnumerable<Users>> GetUserss() =>
            await mediator.Send(new GetAllGenericQueryGR<Users>());


        // GET api/Users/5
        //[AllowAnonymous]
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpGet("{id}")]
        public async Task<Users> GetUsers(Guid id) =>
            await mediator.Send(new GetGenericQueryGR<Users>(id));


        // DELETE: api/Userss/5
        [Authorize(Roles = "TotalControl")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteUsers(Guid id) =>
            await mediator.Send(new RemoveGenericCommandGR<Users>(id));



        // PUT: api/Userss/5
        [Authorize(Roles = "TotalControl")]
        [HttpPut("{id}")]
        public async Task<string> PutUsers([FromRoute] Guid id, [FromBody] Users Users)
        {
            return await mediator.Send(new PutGenericCommandGR<Users>(Users, id));
        }

        // POST api/Users
        [Authorize(Roles = "TotalControl")]
        [HttpPost]
        public async Task<string> PostUsers([FromBody] Users Users) =>
            await mediator.Send(new CreateGenericCommandGR<Users>(Users));

    }
}
