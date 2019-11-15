using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Commands
{
    public class RemoveGenericCommand<T> : IRequest<string> where T : class
    {
        public RemoveGenericCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
    
}
