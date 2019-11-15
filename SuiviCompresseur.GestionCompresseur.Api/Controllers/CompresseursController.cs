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
using SuiviCompresseur.GestionFournisseur.Data.Context;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompresseursController : ControllerBase
    {
        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;
        

        public CompresseursController(CompresseurDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
         
        }


        // GET: api/Compresseurs/active
        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<Compresseur>> GetActiveCompresseur()
        {
            var list = _context.Compresseurs.Where(x => x.GetType().Equals(typeof(Compresseur))).ToList();
            return list.Where(x => x.Active == true);
        }

        // GET: api/Compresseurs
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Compresseur>> GetAllCompresseurs()
        {
            var list = _context.Compresseurs.Where(x => x.GetType().Equals(typeof(Compresseur))).ToList();
            return list;
        }


        // GET: api/Compresseurs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Compresseur> GetCompresseur(Guid id) =>
            await mediator.Send(new GetGenericQuery<Compresseur>(id));


        // PUT: api/Compresseurs/5
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPut("{id}")]
        public async Task<string> PutCompresseur([FromRoute] Guid id, [FromBody] Compresseur Compresseur) =>
            await mediator.Send(new PutGenericCommand<Equipement>(id, Compresseur));


        // POST: api/Compresseurs
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPost]
        public async Task<ActionResult<string>> PostCompresseur([FromBody] Compresseur Compresseur)
        {
           // Compresseur.FrequenceEntretiensCompresseur = _db.Fournisseurs.Where(x => x.FournisseurID == Compresseur.FournisseurID).FirstOrDefault().Frequence_Des_Entretiens_Compresseur;
              return await mediator.Send(new CreateGenericCommand<Equipement>(Compresseur));
        }
          


        // DELETE: api/Compresseurs/5
        [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteCompresseur(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<Equipement>(id));
    }
}