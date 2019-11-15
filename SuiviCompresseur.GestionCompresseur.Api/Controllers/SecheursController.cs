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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SuiviCompresseur.GestionFournisseur.Data.Context;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SecheursController : ControllerBase
    {

        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;

        public SecheursController(CompresseurDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;

        }

        // GET: api/Secheurs/active
        //   [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<Secheur>> GetActiveSecheur()
        {
            var list = _context.Secheurs.Where(x => x.GetType().Equals(typeof(Secheur))).ToList();
            return list.Where(x => x.Active == true);
        }

        // GET: api/Secheurs
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Secheur>> GetAllSecheur()
        {
            var list = _context.Secheurs.Where(x => x.GetType().Equals(typeof(Secheur))).ToList();
            return list;
        }


        // GET: api/Secheurs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Secheur> GetSecheur(Guid id) =>
            await mediator.Send(new GetGenericQuery<Secheur>(id));


        // PUT: api/Secheurs/5
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPut("{id}")]
        public async Task<string> PutSecheur([FromRoute] Guid id, [FromBody] Secheur Secheur) =>
            await mediator.Send(new PutGenericCommand<Equipement>(id, Secheur));


        // POST: api/Secheurs
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPost]
        public async Task<ActionResult<string>> PostSecheur([FromBody] Secheur Secheur)
        {
            //  Secheur.FrequenceEntretiensSecheur = _db.Fournisseurs.Where(x => x.FournisseurID == Secheur.FournisseurID).FirstOrDefault().Frequence_Des_Entretiens_Secheur;
            return await mediator.Send(new CreateGenericCommand<Equipement>(Secheur));
        }



        // DELETE: api/Secheurs/5
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteSecheur(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<Equipement>(id));


    }
}
