using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Commands
{
    public class PutGenericCommand<T> : IRequest<string> where T : class
    {
        public PutGenericCommand(Guid id, T obj)
        {
            Id = id;
            Obj = obj;
        }
        public Guid Id { get; }
        public T Obj { get; }
    }
}