using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class EntretienCompresseursController : ControllerBase
    {

        private readonly IMediator mediator;

        public EntretienCompresseursController( IMediator mediator)
        {   
            this.mediator = mediator;
           
        }


        // GET: api/EntretienCompresseurs
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<EntretienCompresseur>> GetEntretiensCompresseurs() =>
            await mediator.Send(new GetAllGenericQuery<EntretienCompresseur>());


        // GET: api/EntretienCompresseurs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<EntretienCompresseur> GetCompresseurFiliale(Guid id) =>
            await mediator.Send(new GetGenericQuery<EntretienCompresseur>(id));


        // POST: api/EntretienCompresseurs
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> PostEntretienCompresseur([FromBody] EntretienCompresseur entretienCompresseur)
        {
            int result = DateTime.Compare(entretienCompresseur.DateDernierEntretien, DateTime.Now);
            //var EquipementFiliale = _context.EquipementFiliales.Where(c => c.EquipementFilialeID == entretienCompresseur.EquipementFilialeID).FirstOrDefault();
            //int frequence = _context.Equipements.Where(c => c.EquipementID == EquipementFiliale.EquipementID).Select(c => c.FrequenceEntretien).FirstOrDefault();
            //entretienCompresseur.ValeurCompteurProchainEntretien = entretienCompresseur.PriseCompteurDernierEntretien + frequence;
            if (entretienCompresseur.PriseCompteurDernierEntretien > entretienCompresseur.PriseCompteurActuelle)
            {
                return ("la valeur du Prise de compteur lors du dernier Entretien doit être Inférieure ou égale à la valeur du Prise du compteur Actuelle ");

            }
            else if (result > 0)
            {
                return ("La date du dernier Entretien ne doit pas dépasser la date actuelle");
            }
            else
                return await mediator.Send(new CreateGenericCommand<EntretienCompresseur>(entretienCompresseur));


        }


        // PUT: api/EntretienCompresseurs/5
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<string> PutEntretienCompresseur([FromRoute] Guid id, [FromBody] EntretienCompresseur entretienCompresseur)
        {
            int result = DateTime.Compare(entretienCompresseur.DateDernierEntretien, DateTime.Now);
            //var compresseurfiliale = _context.CompresseurFiliales.Where(c => c.CompFilialeID == entretienCompresseur.CompFilialeID).FirstOrDefault();
            //int frequence = _context.FicheCompresseurs.Where(c => c.CompresseurID == compresseurfiliale.CompresseurID).Select(c => c.FrequenceEntretien).FirstOrDefault();
            //entretienCompresseur.ValeurCompteurProchainEntretien = entretienCompresseur.PriseCompteurDernierEntretien + frequence;
            if (entretienCompresseur.PriseCompteurDernierEntretien > entretienCompresseur.PriseCompteurActuelle)
            {
                return ("la valeur du Prise de compteur lors du dernier Entretien doit être Inférieure ou égale à la valeur du Prise du compteur Actuelle ");

            }
            else if (result > 0)
            {
                return ("La date du dernier Entretien ne doit pas dépasser la date actuelle");
            }
            else
                return await mediator.Send(new PutGenericCommand<EntretienCompresseur>(id, entretienCompresseur));
        }


        // DELETE: api/EntretienCompresseurs/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<string> DeleteEntretienCompresseur(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<EntretienCompresseur>(id));



    }
}
