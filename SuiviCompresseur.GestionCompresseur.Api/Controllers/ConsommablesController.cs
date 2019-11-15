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

using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ConsommablesController : ControllerBase
    {
       private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;

        public ConsommablesController(CompresseurDbContext context, IMediator mediator)
        {
            this.mediator = mediator;
            _context = context;
        }


        // GET: api/Consommables

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Consommable>> GetConsommables() =>
             await mediator.Send(new GetAllGenericQuery<Consommable>());


        // GET: api/Consommable/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Consommable> GetConsommable(Guid id) =>
            await mediator.Send(new GetGenericQuery<Consommable>(id));


        // POST: api/Consommable
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpPost]
        public async Task<string> PostConsommable([FromBody] Consommable consommable) 
             {
            consommable.FraisElectriciteMensuel=consommable.ConsommationComp*consommable.PrixUnitaire;

          
          return  await mediator.Send(new CreateGenericCommand<Consommable>(consommable));

            }
        // PUT: api/Consommable/5
        //[AllowAnonymous]
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpPut("{id}")]
        public async Task<string> PutConsommable([FromRoute] Guid id, [FromBody] Consommable consommable)

        {                
  consommable.FraisElectriciteMensuel=consommable.ConsommationComp*consommable.PrixUnitaire;
         return  await mediator.Send(new PutGenericCommand<Consommable>(id, consommable));

        }
        // DELETE: api/Consommable/5
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteConsommable(Guid id) =>
            await 
            mediator.Send(new RemoveGenericCommand<Consommable>(id));

    }
}