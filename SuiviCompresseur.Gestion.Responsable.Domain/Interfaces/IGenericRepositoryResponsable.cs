using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Interfaces
{
    public interface IGenericRepositoryResponsable<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(Guid id);

        string Post(T obj);

        string Delete(Guid id);

        string Put(Guid id, T obj);
    }
}
