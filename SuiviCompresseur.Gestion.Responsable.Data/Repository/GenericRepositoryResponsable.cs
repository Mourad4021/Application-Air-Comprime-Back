using SuiviCompresseur.Gestion.Responsable.Data.Context;
using SuiviCompresseur.Gestion.Responsable.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Data.Repository
{
    public class GenericRepositoryResponsable<T> : IGenericRepositoryResponsable<T> where T : class
    {
        private readonly Gestion_Responsable_DBContext _context;
        private DbSet<T> table = null;

        public GenericRepositoryResponsable(Gestion_Responsable_DBContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }

        public string Delete(Guid id)
        {
            T existing = table.Find(id);
            if (existing == null)
                return " Not Found ";
            else
            {
                table.Remove(existing);
                _context.SaveChanges();
                return "Delete Done";
            }
        }

        public T Get(Guid id)
        {
            T existing = table.Find(id);
            if (existing == null)
                return null;
            else
            {
                return existing;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public string Post(T obj)
        {
            table.Add(obj);
            _context.SaveChanges();
            return "Added done";
        }

        public string Put(Guid id, T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return "Update Done";



         
        }
    }
}
