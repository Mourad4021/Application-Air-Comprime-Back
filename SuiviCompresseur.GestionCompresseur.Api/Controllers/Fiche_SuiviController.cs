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
using SuiviCompresseur.GestionCompresseur.Domain.Enum;
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


        // GET: api/Fiche_Suivis
        [AllowAnonymous]
        [HttpGet("triM/{last}")]
        public IOrderedQueryable<Fiche_Suivi> GetAllOG(int last)
        {
            DateTime month = DateTime.Now;
            DateTime lastMonthDate;
            int annee = month.Year;
            int mois = month.Month - 1;
            if (mois == 0)
            {
                mois = 12;
                annee--;
            }
            int mois2 = mois - last;
            int annee2 = annee;
            if (mois2 == 0)
            {
                mois2 = 12;
                annee2--;
            }
            int numberOfDays = DateTime.DaysInMonth(annee, mois);
            int numberOfDays2 = DateTime.DaysInMonth(annee2, mois2);
            DateTime date = new DateTime(annee, mois, numberOfDays);
            DateTime date2 = new DateTime(annee2, mois2, numberOfDays2);
            //return _context.Fiche_Suivis.Where(x => x.Date.CompareTo(date)>0).OrderBy(p => p.Date).GroupBy(g => g.EquipementFilialeID);
            if (last == 0)
            {
                return _context.Fiche_Suivis.Where(x => x.Date.CompareTo(date) > 0).OrderBy(p => p.Date).OrderBy(g => g.EquipementFilialeID).OrderBy(h => h.EquipementFiliale.FilialeID);
            }
            else
            {
                date = new DateTime(annee, mois, 1);
                date2 = new DateTime(annee, mois, numberOfDays);
                return _context.Fiche_Suivis.Where(x => x.Date.CompareTo(date2) <= 0 && x.Date.CompareTo(date) >= 0).OrderBy(p => p.Date).OrderBy(g => g.EquipementFilialeID).OrderBy(h => h.EquipementFiliale.FilialeID);
            }



        }

        // GET: api/Fiche_Suivis/LastFicheSuiviByCompresseurFililaeId
        [AllowAnonymous]
        [HttpGet("LastFicheSuiviByCompresseurFililaeId")]
        public Fiche_Suivi LastFicheSuiviByCompresseurFililaeId(Guid idEqupementFiliale)
        {
            return _context.Fiche_Suivis.Where(fs => fs.EquipementFilialeID == idEqupementFiliale).OrderBy(p => p.Date).LastOrDefault();
        }


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
            StringValues Index_Debitmetre;
            StringValues FraisEntretienReparation;
            StringValues Date;
            StringValues Etat;
            StringValues PointDeRoseeDuSecheur;
            StringValues THuileC;
            StringValues TempsArret;
            StringValues Nbre_Heurs_Total;
            StringValues TSecheurC;
            StringValues Remarques;
            StringValues PriseCompteurDernierEntretien;
            StringValues Nbre_Heurs_Charge;
            StringValues Index_Electrique;
            StringValues TypeDernierEntretien;
            StringValues NombreHeuresProductionUsineLeJourPrecedent;
            StringValues NombreDeJoursOuvrablesDuMois;
       

            //
            Request.Form.TryGetValue("Date", out Date);
            Request.Form.TryGetValue("EquipementFilialeID", out EquipementFilialeID );
            Request.Form.TryGetValue("Nbre_Heurs_Total", out Nbre_Heurs_Total);
            Request.Form.TryGetValue("Nbre_Heurs_Charge", out Nbre_Heurs_Charge);
            Request.Form.TryGetValue("TempsArret", out TempsArret);
            Request.Form.TryGetValue("Etat", out Etat);
            Request.Form.TryGetValue("PointDeRoseeDuSecheur", out PointDeRoseeDuSecheur);
            Request.Form.TryGetValue("Index_Debitmetre", out Index_Debitmetre);
            Request.Form.TryGetValue("FraisEntretienReparation", out FraisEntretienReparation);
            Request.Form.TryGetValue("PriseCompteurDernierEntretien", out PriseCompteurDernierEntretien);
            Request.Form.TryGetValue("Remarques", out Remarques);
            Request.Form.TryGetValue("THuileC", out THuileC);
            Request.Form.TryGetValue("Index_Electrique", out Index_Electrique);
            Request.Form.TryGetValue("TypeDernierEntretien", out TypeDernierEntretien);
            Request.Form.TryGetValue("NombreHeuresProductionUsineLeJourPrecedent", out NombreHeuresProductionUsineLeJourPrecedent);
            Request.Form.TryGetValue("NombreDeJoursOuvrablesDuMois", out NombreDeJoursOuvrablesDuMois);

            if (Request.Form.TryGetValue("NombreDeJoursOuvrablesDuMois", out NombreDeJoursOuvrablesDuMois)== false)
            {
                NombreDeJoursOuvrablesDuMois = "0";
            }
            

            var fiche_Suivi = new Fiche_Suivi()
            {
                EquipementFilialeID  = Guid.Parse(EquipementFilialeID ),   
                FraisEntretienReparation = Convert.ToInt32(FraisEntretienReparation),
                Date = Convert.ToDateTime(Date),
                Etat = ConvertFromStringToListeEtat(Etat),
                PointDeRoseeDuSecheur = PointDeRoseeDuSecheur,
                THuileC = Convert.ToDouble(THuileC),
                TempsArret = Convert.ToDouble(TempsArret),
                Nbre_Heurs_Total = Convert.ToInt32(Nbre_Heurs_Total),
                Remarques = Remarques,
                PriseCompteurDernierEntretien = Convert.ToInt32(PriseCompteurDernierEntretien),
                Nbre_Heurs_Charge = Convert.ToInt32(Nbre_Heurs_Charge),
                Index_Electrique = Convert.ToInt32(Index_Electrique),
                TypeDernierEntretien= ConvertFromStringToTypeEntretien(TypeDernierEntretien),
                NombreHeuresProductionUsineLeJourPrecedent= Convert.ToInt32(NombreHeuresProductionUsineLeJourPrecedent),
                NombreDeJoursOuvrablesDuMois= Convert.ToInt32(NombreDeJoursOuvrablesDuMois),
                Index_Debitmetre= Convert.ToInt32(Index_Debitmetre)
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
        ListeTypeEntretien ConvertFromStringToTypeEntretien(string etat)
        {
            switch (etat)
            {
                case "A":
                    return ListeTypeEntretien.A;
                    break;
                case "B":
                    return ListeTypeEntretien.B;
                    break;
                case "C":
                    return ListeTypeEntretien.C;
                    break;
                default:
                    return ListeTypeEntretien.D;
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
