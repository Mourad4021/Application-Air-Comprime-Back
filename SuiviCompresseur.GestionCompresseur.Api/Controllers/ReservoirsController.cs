using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservoirsController : ControllerBase
    {
        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;

        public ReservoirsController(CompresseurDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
        }

        // GET: api/Reservoirs/active
        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<Reservoir>> GetActiveReservoir()
        {
            var list = _context.Reservoirs.Where(x => x.GetType().Equals(typeof(Reservoir))).ToList();
            return list.Where(x => x.Active == true);
        }


        // GET: api/Reservoirs
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Reservoir>> GetAllReservoir()
        {
            var list = _context.Reservoirs.Where(x => x.GetType().Equals(typeof(Reservoir))).ToList();
            return list;
        }


        // GET: api/Reservoirs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Reservoir> GetReservoir(Guid id) =>
            await mediator.Send(new GetGenericQuery<Reservoir>(id));


        // PUT: api/Reservoirs/5
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPut("{id}")]
        public async Task<string> PutReservoir([FromRoute] Guid id, [FromBody] Reservoir Reservoir) =>
            await mediator.Send(new PutGenericCommand<Equipement>(id, Reservoir));


        // POST: api/Reservoirs
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPost]
        public async Task<ActionResult<string>> PostReservoir([FromBody] Reservoir Reservoir) =>
            await mediator.Send(new CreateGenericCommand<Equipement>(Reservoir));


        // DELETE: api/Reservoirs/5
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteReservoir(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<Equipement>(id));
    }
}