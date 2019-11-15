using SuiviCompresseur.GestionResponsable.Data.Context;
using SuiviCompresseur.GestionResponsable.Domain.Interfaces;
using SuiviCompresseur.GestionResponsable.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SuiviCompresseur.GestionResponsable.Data.Repository
{
    public class UtilisateursRepository : IUtilisateursRepository
    {
        private readonly Gestion_Responsable_DBContext _context;

        // Constructor
        public UtilisateursRepository(Gestion_Responsable_DBContext context)
        {
            _context = context;
        }


        // CRUD
        public bool DeleteUtilisateur(int id)
        {
            var Utilisateur = _context.Utilisateurs.Find(id);
            _context.Utilisateurs.Remove(Utilisateur);
             _context.SaveChanges();

            return true;
        }

        public Utilisateur GetUtilisateur(int id)
        {
            var Utilisateur = _context.Utilisateurs.Find(id);

            return Utilisateur;
        }

        public IEnumerable<Utilisateur> GetUtilisateurs()
        {
            return _context.Utilisateurs.ToList();
        }

        public int PostUtilisateur(Utilisateur Utilisateur)
        {
            _context.Utilisateurs.Add(Utilisateur);
            _context.SaveChanges();

            return Utilisateur.UtilisateurID;
        }


        public int PutUtilisateur(int id, Utilisateur utilisateur)
        {


            var entity = _context.Utilisateurs.Find(id);
            if (entity != null)
            {
                entity.Nom = utilisateur.Nom;
                entity.Login = utilisateur.Login;
                entity.MotDePasse = utilisateur.MotDePasse;


                // _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return id;

            }
            else
            {
                return 0;
            }


        }


       
    }
}
