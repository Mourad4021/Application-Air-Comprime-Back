//using SuiviCompresseur.GestionCompresseur.Data.Context;
//using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace SuiviCompresseur.GestionCompresseur.Data.Repository
//{
//    public class CompresseurRepository : ICompresseurRepository
//    {
//        private CompresseurDbContext _db;

//        public CompresseurRepository(CompresseurDbContext db)
//        {
//            _db = db;
//        }

//        public void Add(FournisseurDup fournisseurDup)
//        {
//            _db.FournisseursDup.Add(fournisseurDup);
//            _db.SaveChanges();
//        }

//        public IEnumerable<FournisseurDup> GetFournisseur()
//        {
//            return _db.FournisseursDup;
//        }
//    }
//}
