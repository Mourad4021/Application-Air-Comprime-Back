using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Commands
{
    public class CreateGenericCommand<T> : IRequest<string> where T : class
    {
        public T Obj { get; }
        public CreateGenericCommand(T obj)
        {
            Obj = obj;
        }

    }
}
