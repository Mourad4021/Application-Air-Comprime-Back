
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SuiviCompresseur.GestionResponsable.Domain.Interfaces;
using SuiviCompresseur.GestionResponsable.Data.Context;
using SuiviCompresseur.GestionResponsable.Domain.Models;

namespace SuiviCompresseur.GestionResponsable.Data.Repository

{
    public class FilialesRepository : IFilialesRepository
    {
        private readonly Gestion_Responsable_DBContext _context;

        // Constructor
        public FilialesRepository(Gestion_Responsable_DBContext context)
        {
            _context = context;
        }


        // CRUD
        public bool DeleteFiliale(int id)
        {
            var Filiale = _context.Filiales.Find(id);
            _context.Filiales.Remove(Filiale);
            _context.SaveChanges();

            return true;
        }

        public Filiale GetFiliale(int id)
        {
            var Filiale = _context.Filiales.Find(id);

            return Filiale;
        }

        public IEnumerable<Filiale> GetFiliales()
        {
            return _context.Filiales;
        }

        public int PostFiliale(Filiale filiale)
        {
            _context.Filiales.Add(filiale);
            _context.SaveChanges();

            return filiale.FilialeID;
        }

      

        public int PutFiliale(int id, Filiale filiale)
        {


            var entity = _context.Filiales.Find(id);
            if (entity!=null)
            {
                entity.Nom = filiale.Nom;
                entity.Code = filiale.Code;
         

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

