using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Queries
{
    public class GetGenericQueryGR<T> : IRequest<T> where T : class
    {
        public GetGenericQueryGR(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
