using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using SuiviCompresseur.GestionCompresseur.Application.Interfaces;
using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class GRHsController : ControllerBase
    {
        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;

        public GRHsController(CompresseurDbContext context, IMediator mediator)
        {
            this.mediator = mediator;
            _context = context;
        }


        // GET: api/GRHs
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GRH>> GetGRHs() =>
            await mediator.Send(new GetAllGenericQuery<GRH>());


        // GET: api/GRHs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<GRH> GetGRH(Guid id) =>
            await mediator.Send(new GetGenericQuery<GRH>(id));


        // PUT: api/GRHs/5
        [Authorize(Roles = "Editors , TotalControl , LimitedAccess")]
        [HttpPut("{id}")]
        public async Task<string> PutGRH([FromRoute] Guid id, [FromBody] GRH grh) =>
            await mediator.Send(new PutGenericCommand<GRH>(id, grh));


        // POST: api/GRHs
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPost]
        public async Task<string> PostGRH([FromBody] GRH gRH) =>
            await mediator.Send(new CreateGenericCommand<GRH>(gRH));


        // DELETE: api/GRHs/5
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteGRH(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<GRH>(id));


    }
}
