using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using SuiviCompresseur.GestionCompresseur.Application.Services;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Application.Interfaces;
using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SuiviCompresseur.GestionCompresseur.Domain.Dto;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EquipementFilialesController : ControllerBase
    {

        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;


        public EquipementFilialesController(CompresseurDbContext context, IMediator mediator)
        {
            this.mediator = mediator;
            _context = context;
        }


        // GET: api/EquipementFiliales/active
        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<EquipementFiliale>> GetActiveEquipementFiliale()
        {

            return _context.EquipementFiliales.Where(x => x.Active == true);
        }

        // GET: api/EquipementFiliales
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<EquipementFiliale>> GetEquipementFiliale() =>
            await mediator.Send(new GetAllGenericQuery<EquipementFiliale>());


        // GET: api/EquipementFiliales/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<EquipementFiliale> GetEquipementFiliale(Guid id) =>
            await mediator.Send(new GetGenericQuery<EquipementFiliale>(id));


        // POST: api/EquipementFiliales
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpPost]
        public async Task<string> PostEquipementFiliale([FromBody] EquipementFiliale EquipementFiliale) =>
            await mediator.Send(new CreateGenericCommand<EquipementFiliale>(EquipementFiliale));

        // PUT: api/EquipementFiliales/5
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpPut("{id}")]
        public async Task<string> PutEquipementFiliale([FromRoute] Guid id, [FromBody] EquipementFiliale EquipementFiliale) =>
            await mediator.Send(new PutGenericCommand<EquipementFiliale>(id, EquipementFiliale));

        // DELETE: api/EquipementFiliales/5
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteEquipementFiliale(Guid id) =>
            await mediator.Send(new RemoveGenericCommand<EquipementFiliale>(id));





        // POST: api/CompresseurSecheurFiliales
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpPost("CompresseurSecheurFiliales")]
        public async Task<string> PostCompresseurSecheurFiliales([FromBody] CompresseurSecheurFiliale compresseurSecheurFiliale)
        {
            DateTime DateNow = new DateTime();
            DateNow = DateTime.Now;

            if (compresseurSecheurFiliale.DateAcquisition > DateNow)
            {
                return ("Date d'acquisition invalide");
            }
            else
            {
                IEnumerable<CompresseurSecheurFiliale> EquipementFilaleList = await GetActiveCompresseurSecheurFiliale();
                if (EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.Nom).Contains(compresseurSecheurFiliale.Nom)
                    && EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.NumSerie).Contains(compresseurSecheurFiliale.NumSerie))
                {
                    return ("Nom et NumSerie Existants");
                }
                else if (EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.Nom).Contains(compresseurSecheurFiliale.Nom).Equals(false)
                    && EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.NumSerie).Contains(compresseurSecheurFiliale.NumSerie))
                {
                    return ("NumSerie Existant");
                }
                else if (EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.Nom).Contains(compresseurSecheurFiliale.Nom)
                   && EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.NumSerie).Contains(compresseurSecheurFiliale.NumSerie).Equals(false))
                {
                    return ("Nom Existant");
                }
                else
                {
                    EquipementFiliale equipementFiliale = new EquipementFiliale();

                    equipementFiliale.EquipementFilialeID = compresseurSecheurFiliale.EquipementFilialeID;
                    equipementFiliale.EquipementID = compresseurSecheurFiliale.EquipementID;
                    equipementFiliale.FilialeID = compresseurSecheurFiliale.FilialeID;
                    equipementFiliale.Nom = compresseurSecheurFiliale.Nom;
                    equipementFiliale.Active = compresseurSecheurFiliale.Active;

                    PostEquipementFiliale(equipementFiliale);

                    Equip_Filiales_Comp_Sech equip_Filiales_Comp_Sech = new Equip_Filiales_Comp_Sech();

                    equip_Filiales_Comp_Sech.EFID = equipementFiliale.EquipementFilialeID;
                    equip_Filiales_Comp_Sech.NumSerie = compresseurSecheurFiliale.NumSerie;
                    equip_Filiales_Comp_Sech.PrixAcquisition = compresseurSecheurFiliale.PrixAcquisition;
                    equip_Filiales_Comp_Sech.DateAcquisition = compresseurSecheurFiliale.DateAcquisition;


                    PostEquip_Filiales_Comp_Sech(equip_Filiales_Comp_Sech);

                    return ("Added done");
                }
            }



        }

        // PUT: api/EquipementFiliales/5
        [Authorize(Roles = "LimitedAccess , TotalControl")]
        [HttpPut("CompresseurSecheurFiliales")]
        public async Task<string> PutCompSechFiliale([FromBody] CompresseurSecheurFiliale compresseurSecheurFiliale)
        {
            IEnumerable<CompresseurSecheurFiliale> EquipementFilaleList = await GetActiveCompresseurSecheurFiliale();
            if (EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.Nom).Contains(compresseurSecheurFiliale.Nom)
                && EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.NumSerie).Contains(compresseurSecheurFiliale.NumSerie))
            {
                return ("Nom et NumSerie Existants");
            }
            else if (EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.Nom).Contains(compresseurSecheurFiliale.Nom).Equals(false)
                && EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.NumSerie).Contains(compresseurSecheurFiliale.NumSerie))
            {
                return ("NumSerie Existant");
            }
            else if (EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.Nom).Contains(compresseurSecheurFiliale.Nom)
               && EquipementFilaleList.Where(x => x.FilialeID == compresseurSecheurFiliale.FilialeID).Select(w => w.NumSerie).Contains(compresseurSecheurFiliale.NumSerie).Equals(false))
            {
                return ("Nom Existant");
            }
            else
            {
                EquipementFiliale equipementFiliale = new EquipementFiliale();

                equipementFiliale.EquipementFilialeID = compresseurSecheurFiliale.EquipementFilialeID;
                equipementFiliale.EquipementID = compresseurSecheurFiliale.EquipementID;
                equipementFiliale.FilialeID = compresseurSecheurFiliale.FilialeID;
                equipementFiliale.Nom = compresseurSecheurFiliale.Nom;
                equipementFiliale.Active = compresseurSecheurFiliale.Active;

                PutEquipementFiliale(equipementFiliale.EquipementFilialeID, equipementFiliale);

                Equip_Filiales_Comp_Sech equip_Filiales_Comp_Sech = new Equip_Filiales_Comp_Sech();

                equip_Filiales_Comp_Sech.EFID = equipementFiliale.EquipementFilialeID;
                equip_Filiales_Comp_Sech.NumSerie = compresseurSecheurFiliale.NumSerie;
                equip_Filiales_Comp_Sech.PrixAcquisition = compresseurSecheurFiliale.PrixAcquisition;
                equip_Filiales_Comp_Sech.DateAcquisition = compresseurSecheurFiliale.DateAcquisition;
                equip_Filiales_Comp_Sech.EquipementFilialeCompSechID = compresseurSecheurFiliale.EquipementFilialeCompSechID;

                PutEquip_Filiales_Comp_Sech(equip_Filiales_Comp_Sech.EquipementFilialeCompSechID, equip_Filiales_Comp_Sech);

                return ("Update Done");
            }
        }



        // GET: api/EquipementFiliales/CompresseurSecheurFiliales
        [AllowAnonymous]
        [HttpGet("CompresseurSecheurFiliales")]
        public async Task<IEnumerable<CompresseurSecheurFiliale>> GetActiveCompresseurSecheurFiliale()
        {

            List<EquipementFiliale> CompresseurSecheurFilialeList = new List<EquipementFiliale>();

            var listSecheur = _context.Secheurs.Where(x => x.GetType().Equals(typeof(Secheur))).ToList();
            IEnumerable<Equipement> activeSecheur = listSecheur.Where(x => x.Active == true);
            var listCompresseur = _context.Compresseurs.Where(x => x.GetType().Equals(typeof(Compresseur))).ToList();
            IEnumerable<Equipement> activeCompresseur = listCompresseur.Where(x => x.Active == true);

            foreach (Equipement secheur in listSecheur.ToList())
            {
                foreach (EquipementFiliale equipementFiliale in _context.EquipementFiliales.ToList())
                {
                    if (secheur.EquipementID == equipementFiliale.EquipementID)
                    {
                        CompresseurSecheurFilialeList.Add(equipementFiliale);
                    }
                }
            }
            foreach (Equipement compresseur in listCompresseur.ToList())
            {
                foreach (EquipementFiliale equipementFiliale in _context.EquipementFiliales.ToList())
                {
                    if (compresseur.EquipementID == equipementFiliale.EquipementID)
                    {
                        CompresseurSecheurFilialeList.Add(equipementFiliale);
                    }
                }
            }


            IEnumerable<EquipementFiliale> activeEquipementFiliale = CompresseurSecheurFilialeList.Where(x => x.Active == true);
            List<CompresseurSecheurFiliale> CompresseurSecheurFiliale = new List<CompresseurSecheurFiliale>();


            foreach (var item in activeEquipementFiliale)
            {
                CompresseurSecheurFiliale compresseurSecheurFiliale = new CompresseurSecheurFiliale();
                var compsechfiliale = _context.EquipFilialesCompSeches.Where(x => x.EFID == item.EquipementFilialeID).FirstOrDefault();

                compresseurSecheurFiliale.EquipementFilialeID = item.EquipementFilialeID;
                compresseurSecheurFiliale.EquipementID = item.EquipementID;
                compresseurSecheurFiliale.FilialeID = item.FilialeID;
                compresseurSecheurFiliale.Nom = item.Nom;
                compresseurSecheurFiliale.Active = item.Active;
                compresseurSecheurFiliale.EquipementFilialeCompSechID = compsechfiliale.EquipementFilialeCompSechID;
                compresseurSecheurFiliale.PrixAcquisition = compsechfiliale.PrixAcquisition;
                compresseurSecheurFiliale.DateAcquisition = compsechfiliale.DateAcquisition;
                compresseurSecheurFiliale.NumSerie = compsechfiliale.NumSerie;
                compresseurSecheurFiliale.EFID = compsechfiliale.EFID;

                CompresseurSecheurFiliale.Add(compresseurSecheurFiliale);
            }
            return CompresseurSecheurFiliale;
        }


        // GET: api/EquipementFiliales/ReservoirFiliale
        [AllowAnonymous]
        [HttpGet("ReservoirFiliale")]
        public async Task<IEnumerable<EquipementFiliale>> GetActiveReservoirFiliale()
        {

            List<EquipementFiliale> ReservoirFilialeList = new List<EquipementFiliale>();

            List<Reservoir> listReservoir = new List<Reservoir>();
            listReservoir = _context.Reservoirs.Where(x => x.Active == true).ToList();



            foreach (Reservoir reservoir in listReservoir)
            {
                foreach (EquipementFiliale eqtFiliale in _context.EquipementFiliales)
                {
                    if (reservoir.EquipementID == eqtFiliale.EquipementID)
                    {
                        ReservoirFilialeList.Add(eqtFiliale);
                    }
                }
            }


            IEnumerable<EquipementFiliale> activeReservoirFiliale = ReservoirFilialeList.Where(x => x.Active == true);
            List<EquipementFiliale> equipementFiliale = new List<EquipementFiliale>();


            foreach (var item in activeReservoirFiliale)
            {
                EquipementFiliale EquipementFiliale = new EquipementFiliale();


                EquipementFiliale.EquipementFilialeID = item.EquipementFilialeID;
                EquipementFiliale.EquipementID = item.EquipementID;
                EquipementFiliale.FilialeID = item.FilialeID;
                EquipementFiliale.Nom = item.Nom;
                EquipementFiliale.Active = item.Active;


                equipementFiliale.Add(EquipementFiliale);
            }
            return equipementFiliale;
        }



        // POST: api/Equip_Filiales_Comp_Sech
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPost("compSechFiliale")]
        public async Task<string> PostEquip_Filiales_Comp_Sech([FromBody] Equip_Filiales_Comp_Sech Equip_Filiales_Comp_Sech) =>
            await mediator.Send(new CreateGenericCommand<Equip_Filiales_Comp_Sech>(Equip_Filiales_Comp_Sech));


        // PUT: api/Equip_Filiales_Comp_Sech/5
        [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPut("compSechFiliale{id}")]
        public async Task<string> PutEquip_Filiales_Comp_Sech([FromRoute] Guid id, [FromBody] Equip_Filiales_Comp_Sech Equip_Filiales_Comp_Sech) =>
            await mediator.Send(new PutGenericCommand<Equip_Filiales_Comp_Sech>(id, Equip_Filiales_Comp_Sech));

        // GET: api/EquipementFiliales/DateNow
        [AllowAnonymous]
        [HttpGet("DateNow")]
        public async Task<string> GetDateNow()
        {

            return DateTime.Now.Date.ToString("d");
        }




        // GET: api/EquipementFiliales/CompresseursFiliales
        [AllowAnonymous]
        [HttpGet("CompresseursFiliales")]
        public async Task<IEnumerable<CompresseurSecheurFiliale>> GetActiveCompresseursFiliale()
        {

            List<EquipementFiliale> CompresseurFilialeList = new List<EquipementFiliale>();

           
            var listCompresseur = _context.Compresseurs.Where(x => x.GetType().Equals(typeof(Compresseur))).ToList();
            IEnumerable<Equipement> activeCompresseur = listCompresseur.Where(x => x.Active == true);

            foreach (Equipement compresseur in listCompresseur.ToList())
            {
                foreach (EquipementFiliale equipementFiliale in _context.EquipementFiliales.ToList())
                {
                    if (compresseur.EquipementID == equipementFiliale.EquipementID)
                    {
                        CompresseurFilialeList.Add(equipementFiliale);
                    }
                }
            }


            IEnumerable<EquipementFiliale> activeEquipementFiliale = CompresseurFilialeList.Where(x => x.Active == true);
            List<CompresseurSecheurFiliale> CompresseurFiliale = new List<CompresseurSecheurFiliale>();


            foreach (var item in activeEquipementFiliale)
            {
                CompresseurSecheurFiliale compresseurSecheurFiliale = new CompresseurSecheurFiliale();
                var compsechfiliale = _context.EquipFilialesCompSeches.Where(x => x.EFID == item.EquipementFilialeID).FirstOrDefault();

                compresseurSecheurFiliale.EquipementFilialeID = item.EquipementFilialeID;
                compresseurSecheurFiliale.EquipementID = item.EquipementID;
                compresseurSecheurFiliale.FilialeID = item.FilialeID;
                compresseurSecheurFiliale.Nom = item.Nom;
                compresseurSecheurFiliale.Active = item.Active;
                compresseurSecheurFiliale.EquipementFilialeCompSechID = compsechfiliale.EquipementFilialeCompSechID;
                compresseurSecheurFiliale.PrixAcquisition = compsechfiliale.PrixAcquisition;
                compresseurSecheurFiliale.DateAcquisition = compsechfiliale.DateAcquisition;
                compresseurSecheurFiliale.NumSerie = compsechfiliale.NumSerie;
                compresseurSecheurFiliale.EFID = compsechfiliale.EFID;

                CompresseurFiliale.Add(compresseurSecheurFiliale);
            }
            return CompresseurFiliale;
        }

    }
}

