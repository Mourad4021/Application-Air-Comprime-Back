using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.DTOs;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Data.Repository
{
    public class FicheSuiviRepository : IFiches_SuiviRepository
    {
         private CompresseurDbContext _db;

        public FicheSuiviRepository(CompresseurDbContext db)
        {
            _db = db;
        }
    

        public Guid AddFicheSuivi(Fiche_Suivi fiche_Suivi)
        {
           
            _db.Fiche_Suivis.Add(fiche_Suivi);
            _db.SaveChanges();
            return fiche_Suivi.FicheSuiviID;
        }
    }
}
