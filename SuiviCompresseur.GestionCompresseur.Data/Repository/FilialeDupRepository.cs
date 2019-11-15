
//using SuiviCompresseur.GestionCompresseur.Data.Context;
//using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SuiviCompresseur.GestionCompresseur.Data.Repository
//{
//    public class FilialeDupRepository : IFilialeDupRepository
//    {
//        private readonly CompresseurDbContext _context;

//        // Constructor
//        public FilialeDupRepository(CompresseurDbContext context)
//        {
//            _context = context;
//        }

//        public IEnumerable<FilialeDup> GetFilialesDup()
//        {
//            return _context.FilialesDup;
//        }

//        public void Add(FilialeDup filialeDup)
//        {
//            _context.FilialesDup.Add(filialeDup);
//            _context.SaveChanges();
//        }
//    }
//}
