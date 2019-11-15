using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Interfaces
{
    public interface IFournisseurRepository
    {
        IEnumerable<Fournisseur> GetFournisseurs();
        string AddF(Fournisseur fournisseur);
        Fournisseur GetFournisseur(Guid id);

        string DeleteFournisseurs(Guid id);

        string PutFournisseurs(Guid id, Fournisseur fournisseur);
    }
}
