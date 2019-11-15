using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Equip_Filiales_Comp_SechController : ControllerBase
    {
        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;

        public Equip_Filiales_Comp_SechController(CompresseurDbContext context, IMediator mediator)
        {
            this.mediator = mediator;
            _context = context;
        }


        // GET: api/Equip_Filiales_Comp_Sech
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Equip_Filiales_Comp_Sech>> GetEquip_Filiales_Comp_Sechs() =>
            await mediator.Send(new GetAllGenericQuery<Equip_Filiales_Comp_Sech>());


        // GET: api/Equip_Filiales_Comp_Sech/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Equip_Filiales_Comp_Sech> GetEquip_Filiales_Comp_Sech(Guid id) =>
            await mediator.Send(new GetGenericQuery<Equip_Filiales_Comp_Sech>(id));


        // PUT: api/Equip_Filiales_Comp_Sech/5
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPut("{id}")]
        public async Task<string> PutEquip_Filiales_Comp_Sech([FromRoute] Guid id, [FromBody] Equip_Filiales_Comp_Sech Equip_Filiales_Comp_Sech) =>
            await mediator.Send(new PutGenericCommand<Equip_Filiales_Comp_Sech>(id, Equip_Filiales_Comp_Sech));




        // DELETE: api/Equip_Filiales_Comp_Sech/5
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteGRH(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<Equip_Filiales_Comp_Sech>(id));
    }
}