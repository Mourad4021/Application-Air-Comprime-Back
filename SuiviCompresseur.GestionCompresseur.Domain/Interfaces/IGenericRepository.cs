using System;
using System.Collections.Generic;
using System.Text;
using SuiviCompresseur.GestionCompresseur.Domain.Models;

namespace SuiviCompresseur.GestionCompresseur.Domain.Interfaces
{
   public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(Guid id);

        string Post(T obj);

        string Delete(Guid id);

        string Put(Guid id, T obj);
        
    }
}
