using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Queries
{
    public class GetGenericQuery<T> : IRequest<T> where T: class
    {
        public GetGenericQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
  