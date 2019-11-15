using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Commands
{
    public class CreateGenericCommandGR<T> : IRequest<string> where T: class
    {
        public CreateGenericCommandGR(T obj)
        {
            Obj = obj;
        }
        public T Obj { get; }
    }
}
