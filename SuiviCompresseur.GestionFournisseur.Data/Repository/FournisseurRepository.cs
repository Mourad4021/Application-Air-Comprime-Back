using SuiviCompresseur.GestionFournisseur.Data.Context;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Data.Repository
{
    public class FournisseurRepository : IFournisseurRepository
    {
        private FournisseurDbContext _db;

        public FournisseurRepository(FournisseurDbContext db)
        {
            _db = db;
        }

        public string AddF(Fournisseur fournisseur)
        {
            _db.Fournisseurs.Add(fournisseur);
            _db.SaveChanges();
            return "Aded done";
        }

        public string DeleteFournisseurs(Guid id)
        {
            var Fournisseur = _db.Fournisseurs.Find(id);

            if (Fournisseur == null)
                return "Fournisseur Don't Exist";
            else
            {
                _db.Fournisseurs.Remove(Fournisseur);
                _db.SaveChanges();
                return "Delete Done";
            }
        }

        public IEnumerable<Fournisseur> GetFournisseurs()
        {
            return _db.Fournisseurs;
        }

        public Fournisseur GetFournisseur(Guid id)
        {

            var fournisseur = _db.Fournisseurs.Find(id);

            return fournisseur;
        }

        public string PutFournisseurs(Guid id, Fournisseur fournisseur)
        {
            var entity = _db.Fournisseurs.Find(id);
            if (entity != null)
            {
                entity.Nom = fournisseur.Nom;
                entity.Constructeur = fournisseur.Constructeur;
                entity.Frequence_Des_Entretiens_Compresseur = fournisseur.Frequence_Des_Entretiens_Compresseur; 
                entity.Frequence_Des_Entretiens_Secheur = fournisseur.Frequence_Des_Entretiens_Secheur;

                entity.Adresse = fournisseur.Adresse;
                entity.Email = fournisseur.Email;
                entity.Active = fournisseur.Active;
                
                _db.SaveChanges();
                return "Update" + id;
            }
            else
            {
                return "Fournisseur don't exist";
            }
        }
    }
}
