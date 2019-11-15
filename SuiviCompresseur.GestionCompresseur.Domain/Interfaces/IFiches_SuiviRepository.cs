using SuiviCompresseur.GestionCompresseur.Domain.DTOs;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Interfaces
{
    public interface IFiches_SuiviRepository
    {
        //IEnumerable<Fiche_Suivi> GetFiche_Suivis();

        //Fiche_Suivi GetFiche_Suivi(Guid id);

        //string PostFiche_Suivi(Fiche_Suivi fiche_Suivi);

        //string DeleteFiche_Suivi(Guid id);

        //string PutFiche_Suivi(Guid id, Fiche_Suivi fiche_Suivi);
        Guid AddFicheSuivi(Fiche_Suivi fiche_Suivi);
    }
}
