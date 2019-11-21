using SuiviCompresseur.GestionFournisseur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuiviCompresseur.GestionFournisseur.Application.Models;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using SuiviCompresseur.GestionFournisseur.Data.Context;

namespace SuiviCompresseur.GestionFournisseur.Api.Controllers
{
    [Route("api/[controller]")]
   [ApiController]
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GestionFournisseurController : ControllerBase
    {
        private readonly IFournisseurService _fournisseurService;
        private readonly IFournisseurRepository _db;
        private readonly FournisseurDbContext _context;

        public GestionFournisseurController(IFournisseurService fournisseurService, IFournisseurRepository db, FournisseurDbContext context)
        {
            _fournisseurService = fournisseurService;
            _db = db;
            _context = context;
        }

        // GET api/Fournisseur        
     [Authorize(Roles = "Editors , TotalControl , LimitedAccess")]
        [HttpGet]
        public Task<IEnumerable<Fournisseur>> Get()
        {
            return _fournisseurService.GetFournisseurs();
            
        }


        [Authorize(Roles = "Editors , TotalControl")]
        [HttpPost]
        public string Post()
        {

            
            // FournisseurFormFile.

            StringValues Nom;
            StringValues Constructeur;
            StringValues Frequence_Des_Entretiens_Compresseur;
            StringValues Frequence_Des_Entretiens_Secheur;
            StringValues Adresse;
            StringValues Email;


            //
            Request.Form.TryGetValue("Nom", out Nom);
            Request.Form.TryGetValue("Constructeur", out Constructeur);
            Request.Form.TryGetValue("Frequence_Des_Entretiens_Compresseur", out Frequence_Des_Entretiens_Compresseur);
            Request.Form.TryGetValue("Frequence_Des_Entretiens_Secheur", out Frequence_Des_Entretiens_Secheur);
            Request.Form.TryGetValue("Adresse", out Adresse);
            Request.Form.TryGetValue("Email", out Email);


            var fournisseur = new Fournisseur()
            {
                Nom = Convert.ToString(Nom),
                Constructeur = Convert.ToString(Constructeur),
                Frequence_Des_Entretiens_Compresseur = int.Parse(Frequence_Des_Entretiens_Compresseur),
                Frequence_Des_Entretiens_Secheur = int.Parse(Frequence_Des_Entretiens_Secheur),
                Adresse = Convert.ToString(Adresse),
                Email = Convert.ToString(Email),
                Active = true

            };
            //Request.Form.Files

            _fournisseurService.Creation(fournisseur);
            //_context.EntretienReservoirs.Add(EntretienReservoir);
            //_context.SaveChanges();
            foreach (IFormFile formFile in Request.Form.Files)
            {

                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                         formFile.CopyToAsync(stream);
                    }
                }

                var tempAttachement = new AttachementFournisseur();
                _context.AttachementFournisseurs.Add(tempAttachement);

                try
                {
                    var file = formFile;
                    var folderName = Path.Combine("Ressources", "FournisseurFiles");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (file.Length > 0)
                    {
                        var OriginFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var OriginFileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1, ((file.FileName.Length - 1) - file.FileName.LastIndexOf('.')));
                        var fileName = "pieceJointeFR" + tempAttachement.AttachmentFournisseurId;
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }


                        //create attachement for DB table
                        tempAttachement.AttachmentNameF = fileName;
                        tempAttachement.AttachmentFileFormatF = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1, ((file.FileName.Length - 1) - file.FileName.LastIndexOf('.')));
                        tempAttachement.AttachmentOriginFileNameF = OriginFileName;
                        tempAttachement.FournisseurID = fournisseur.FournisseurID;
                        tempAttachement.AttachmentPhysicalPathF = fullPath;


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
            return "Aded done";
        }
    


        // GET: api/fournisseur/5
        //[AllowAnonymous]
        [Authorize(Roles = "Editors , TotalControl , LimitedAccess")]
        [HttpGet("{id}")]
        public Task<Fournisseur> GetFournisseur(Guid id)
        {
            return _fournisseurService.GetFournisseur(id);
        }


        // PUT: api/fournisseur/5
     [Authorize(Roles = "Editors , TotalControl")]
        [HttpPut("{id}")]
        public Task<string> PutFournisseur(Guid id, Fournisseur fournisseur)
        {
            return _fournisseurService.PutFournisseurs(id, fournisseur);
        }



        // DELETE: api/fournisseur/5
      [Authorize(Roles = "Editors , TotalControl")]
        [HttpDelete("{id}")]
        public Task<string> DeleteFournisseur(Guid id)
        {
            return _fournisseurService.DeleteFournisseurs(id);
        }
    }
}
