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
using SuiviCompresseur.GestionCompresseur.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SuiviCompresseur.GestionCompresseur.Domain.DTOs;
using SuiviCompresseur.GestionCompresseur.Domain.Commands.ficheSuivi;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Net.Http.Headers;
//using SuiviCompresseur.GestionCompresseur.Domain.Queries.Fiche_SuiviQueries;
//using SuiviCompresseur.GestionCompresseur.Domain.Commands.Fiche_SuiviCommands;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class Fiche_SuiviController : ControllerBase
    {

        private readonly CompresseurDbContext _context;
        private readonly IMediator mediator;

        public Fiche_SuiviController(CompresseurDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
        }


        // GET: api/Fiche_Suivis
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Fiche_Suivi>> GetFiche_Suivis() =>

            await mediator.Send(new GetAllGenericQuery<Fiche_Suivi>());


        // GET: api/Fiche_Suivis/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Fiche_Suivi> GetFiche_Suivi(Guid id) =>

            await mediator.Send(new GetGenericQuery<Fiche_Suivi>(id));

        [AllowAnonymous]
        // PUT: api/Fiche_Suivis/5
        //hajer// [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPut("{id}")]
        public async Task<string> PutFiche_Suivi([FromRoute] Guid id, [FromBody] Fiche_Suivi fiche_Suivi)
        {
            ValidationContraint validationFiche_Suivi = new ValidationContraint(_context);
            
            string testval = validationFiche_Suivi.testPut(fiche_Suivi,id);

            if (testval == "true")
            {
                return await mediator.Send(new PutGenericCommand<Fiche_Suivi>(id, fiche_Suivi));
            }
            else
                return testval;
        }


        [AllowAnonymous]
        // POST: api/Fiche_Suivis
     //hajer    [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpPost]
        public async Task<ActionResult<string>> PostFiche_Suivi(IFormFile fiche_SuiviFormFile)
        {
           ValidationContraint validationFiche_Suivi = new ValidationContraint(_context);

            // fiche_SuiviFormFile.

            StringValues EquipementFilialeID;
            StringValues CourantAbsorbePhase;
            StringValues FraisEntretienReparation;
            StringValues Date;
            StringValues Etat;
            StringValues FrequenceEentretienDeshuileur;
            StringValues THuileC;
            StringValues TempsArret;
            StringValues Nbre_Heurs_Total;
            StringValues TSecheurC;
            StringValues Remarques;
            StringValues PriseCompteur;
            StringValues Nbre_Heurs_Charge;
            StringValues Index_Electrique;

            //
            Request.Form.TryGetValue("equipementFilialeID", out EquipementFilialeID );
            Request.Form.TryGetValue("CourantAbsorbePhase", out CourantAbsorbePhase);
            Request.Form.TryGetValue("FraisEntretienReparation", out FraisEntretienReparation);
            Request.Form.TryGetValue("Date", out Date);
            Request.Form.TryGetValue("Etat", out Etat);
            Request.Form.TryGetValue("FrequenceEentretienDeshuileur", out FrequenceEentretienDeshuileur);
            Request.Form.TryGetValue("THuileC", out THuileC);
            Request.Form.TryGetValue("TempsArret", out TempsArret);
            Request.Form.TryGetValue("Nbre_Heurs_Total", out Nbre_Heurs_Total);
            Request.Form.TryGetValue("Remarques", out Remarques);
            Request.Form.TryGetValue("PriseCompteur", out PriseCompteur);
            Request.Form.TryGetValue("Nbre_Heurs_Charge", out Nbre_Heurs_Charge);
            Request.Form.TryGetValue("Index_Electrique", out Index_Electrique);
            Request.Form.TryGetValue("TSecheurC", out TSecheurC);

            var fiche_Suivi = new Fiche_Suivi()
            {
                EquipementFilialeID  = Guid.Parse(EquipementFilialeID ),
                CourantAbsorbePhase = Convert.ToInt32(CourantAbsorbePhase),
                FraisEntretienReparation = Convert.ToInt32(FraisEntretienReparation),
                Date = Convert.ToDateTime(Date),
                Etat = ConvertFromStringToListeEtat(Etat),
                FrequenceEentretienDeshuileur = FrequenceEentretienDeshuileur,
                THuileC = Convert.ToDouble(THuileC),
                TempsArret = Convert.ToDouble(TempsArret),
                Nbre_Heurs_Total = Convert.ToInt32(Nbre_Heurs_Total),
                TSecheurC = TSecheurC,
                Remarques = Remarques,
                PriseCompteur = Convert.ToDouble(PriseCompteur),
                Nbre_Heurs_Charge = Convert.ToInt32(Nbre_Heurs_Charge),
                Index_Electrique = Convert.ToInt32(Index_Electrique),
            };
            //Request.Form.Files
            string testval = validationFiche_Suivi.testPost(fiche_Suivi);

            if (testval == "true")
            {
                // fiche_Suivi.FicheSuiviID= await mediator.Send(new CreateFicheSuiviCommand(fiche_Suivi));
                _context.Fiche_Suivis.Add(fiche_Suivi);
                _context.SaveChanges();
                foreach (IFormFile formFile in Request.Form.Files)
                {

                    if (formFile.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }

                    var tempAttachement = new Attachment();
                    _context.Attachments.Add(tempAttachement);
                   
                    try
                    {
                        var file = formFile;
                        var folderName = Path.Combine("Resources", "FicheSuiviFiles");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        if (file.Length > 0)
                        {
                            var OriginFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var OriginFileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1, ((file.FileName.Length - 1) - file.FileName.LastIndexOf('.')));
                            var fileName = "pieceJointeFS" +tempAttachement.AttachmentId;
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }


                            //create attachement for DB table
                            tempAttachement.AttachmentName = fileName;
                            tempAttachement.AttachmentFileFormat=file.FileName.Substring(file.FileName.LastIndexOf('.') + 1, ((file.FileName.Length - 1) - file.FileName.LastIndexOf('.')));
                            tempAttachement.AttachmentOriginFileName = OriginFileName;
                            tempAttachement.FicheSuiviID = fiche_Suivi.FicheSuiviID;
                            tempAttachement.AttachmentPhysicalPath = fullPath;


                            //assign attachement to fichSuivi
                            _context.Entry(tempAttachement).State = EntityState.Added;
                            _context.SaveChanges();



                        }
                        _context.SaveChanges();

                    }

                    catch (Exception ex)
                    {

                    }
                }
                return "Added done";
            }
            else
                return testval;
        }

        ListeEtat ConvertFromStringToListeEtat(string etat)
        {
            switch (etat)
            {
                case "En_marche":
                    return ListeEtat.En_marche;
                    break;
                case "Reserve":
                    return ListeEtat.Reserve;
                    break;
                default:
                    return ListeEtat.En_panne ;
                    break;
            }
        }
        [AllowAnonymous]
        // DELETE: api/Fiche_Suivis/5
     //  hajer [Authorize(Roles = "TotalControl , LimitedAccess")]
        [HttpDelete("{id}")]
        public async Task<string> DeleteFiche_Suivi(Guid id)
        {
            var Attachements = _context.Attachments.Where(c => c.FicheSuiviID == id).ToList();


            foreach (var item in Attachements)
            {
                var folderName = Path.Combine("Resources", "FicheSuiviFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = "pieceJointeFS" + item.AttachmentId;
                if (System.IO.File.Exists(Path.Combine(pathToSave, fileName)))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(Path.Combine(pathToSave, fileName));

                }


            }
            _context.Attachments.RemoveRange(Attachements);
            _context.SaveChanges();
            return await mediator.Send(new RemoveGenericCommand<Fiche_Suivi>(id));
        }
            






    }
}
