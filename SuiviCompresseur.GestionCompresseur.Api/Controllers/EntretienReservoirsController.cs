using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntretienReservoirsController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly CompresseurDbContext _context;

        public EntretienReservoirsController( IMediator mediator, CompresseurDbContext context)
        {

            this.mediator = mediator;
            _context = context;
        }

        // GET: api/EntretienReservoirs
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<EntretienReservoir>> GetEntretiensReservoirs() =>
            await mediator.Send(new GetAllGenericQuery<EntretienReservoir>());


        // GET: api/EntretienReservoirs/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<EntretienReservoir> GetEntretienReservoir(Guid id) =>
            await mediator.Send(new GetGenericQuery<EntretienReservoir>(id));


        // PUT: api/EntretienReservoirs/5
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<string> PutEntretienReservoir([FromRoute] Guid id, [FromBody] EntretienReservoir entretienReservoir) =>
            await mediator.Send(new PutGenericCommand<EntretienReservoir>(id, entretienReservoir));


        // POST: api/EntretienReservoirs
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> PostEntretienReservoir()
        {
            // EntretienReservoirFormFile.

            StringValues EquipementFilialeID;
            StringValues NatureVisite;
            StringValues DerniereVisite;
            StringValues ProchaineVisite;
            StringValues Commentaires;
       

            //
            Request.Form.TryGetValue("EquipementFilialeID", out EquipementFilialeID);
            Request.Form.TryGetValue("NatureVisite", out NatureVisite);
            Request.Form.TryGetValue("DerniereVisite", out DerniereVisite);
            Request.Form.TryGetValue("ProchaineVisite", out ProchaineVisite);
            Request.Form.TryGetValue("Commentaires", out Commentaires);
    

            var EntretienReservoir = new EntretienReservoir()
            {
                EquipementFilialeID = Guid.Parse(EquipementFilialeID),
                NatureVisite = ConvertFromStringToListeNatureVisite(NatureVisite),
                DerniereVisite = Convert.ToDateTime(DerniereVisite),
                ProchaineVisite = Convert.ToDateTime(ProchaineVisite),
                Commentaires = Convert.ToString(Commentaires),
                
            };
            //Request.Form.Files

             await mediator.Send(new CreateGenericCommand<EntretienReservoir>(EntretienReservoir));
            //_context.EntretienReservoirs.Add(EntretienReservoir);
            //_context.SaveChanges();
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
                        var folderName = Path.Combine("Resources", "EntretienReservoirFiles");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        if (file.Length > 0)
                        {
                            var OriginFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var OriginFileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1, ((file.FileName.Length - 1) - file.FileName.LastIndexOf('.')));
                            var fileName = "pieceJointeER" + tempAttachement.AttachmentId;
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }


                            //create attachement for DB table
                            tempAttachement.AttachmentName = fileName;
                            tempAttachement.AttachmentFileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1, ((file.FileName.Length - 1) - file.FileName.LastIndexOf('.')));
                            tempAttachement.AttachmentOriginFileName = OriginFileName;
                            tempAttachement.EntretienReservoirID = EntretienReservoir.EntretienReservoirID;
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

        NatureVisite ConvertFromStringToListeNatureVisite(string etat)
        {
            switch (etat)
            {
                case "Interieure":
                    return NatureVisite.Interieure;
                    break;
                case "Exterieure":
                    return NatureVisite.Exterieure;
                    break;
                default:
                    return NatureVisite.Officielle;
                    break;
            }
        }




        // DELETE: api/EntretienReservoirs/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<string> DeleteEntretienReservoir(Guid id)
        {
            var Attachements = _context.Attachments.Where(c => c.EntretienReservoirID == id).ToList();

            foreach (var item in Attachements)
            {  var folderName = Path.Combine("Resources", "EntretienReservoirFiles");
  var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = "pieceJointeER" + item.AttachmentId;
                if (System.IO.File.Exists(Path.Combine(pathToSave, fileName)))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(Path.Combine(pathToSave, fileName));
          
                }

              
                  
                 
                
               
            }
            _context.Attachments.RemoveRange(Attachements);
            _context.SaveChanges();
                
            
            return  await mediator.Send(new RemoveGenericCommand<EntretienReservoir>(id));
        }

        [AllowAnonymous]
        [HttpGet("date")]
        public async Task<DateTime> GetDateTime()
        {
            return DateTime.Now;
        }

    }
}