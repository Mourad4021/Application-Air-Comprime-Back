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
using SuiviCompresseur.GestionCompresseur.Data.Repository;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EquipementsController : ControllerBase
    {
        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;
        private readonly SecheursController secheursController;
        private readonly CompresseursController compresseursController;
        public EquipementsController(CompresseurDbContext context, IMediator mediator)
        {
            this.mediator = mediator;
            _context = context;
        }


        // GET: api/Equipements/active
        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<Equipement>> GetActiveEquipement()
        {
            var list = _context.Equipements.Where(x => x.GetType().Equals(typeof(Equipement))).ToList();
            return list.Where(x => x.Active == true);
        }

        // GET: api/Equipements
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Equipement>> GetEquipements() =>
            await mediator.Send(new GetAllGenericQuery<Equipement>());


        // GET: api/Equipements/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Equipement> GetEquipement(Guid id) =>
            await mediator.Send(new GetGenericQuery<Equipement>(id));


        // PUT: api/Equipements/5
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPut("{id}")]
        public async Task<string> PutEquipement([FromRoute] Guid id, [FromBody] Equipement Equipement) =>
            await mediator.Send(new PutGenericCommand<Equipement>(id, Equipement));


        // POST: api/Equipements
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPost]
        public async Task<string> PostEquipement([FromBody] Equipement Equipement) =>
            await mediator.Send(new CreateGenericCommand<Equipement>(Equipement));


        // DELETE: api/Equipements/5
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteEquipement(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<Equipement>(id));


    }
}