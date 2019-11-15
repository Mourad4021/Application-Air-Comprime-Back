using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Commands
{
    public class RemoveGenericCommandGR<T> :IRequest<string> where T : class
    {
        public RemoveGenericCommandGR(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
