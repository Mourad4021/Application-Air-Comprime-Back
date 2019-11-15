using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Commands
{
    public class PutGenericCommandGR<T> : IRequest<string> where T : class
    {
        public PutGenericCommandGR(T obj, Guid id)
        {
            Id = id;
            Obj = obj;
        }

        public Guid Id { get; }
        public T Obj { get; }

    }
}
