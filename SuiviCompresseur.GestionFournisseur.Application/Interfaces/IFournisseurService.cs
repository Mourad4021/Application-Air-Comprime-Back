using SuiviCompresseur.GestionFournisseur.Application.Models;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionFournisseur.Application.Interfaces
{
    public interface IFournisseurService
    {
        Task<IEnumerable<Fournisseur>> GetFournisseurs();
        Task<string> Creation(Fournisseur fournisseur);
        Task<Fournisseur> GetFournisseur(Guid id);

        Task<string> DeleteFournisseurs(Guid id);

        Task<string> PutFournisseurs(Guid id, Fournisseur fournisseur);
    }
}
