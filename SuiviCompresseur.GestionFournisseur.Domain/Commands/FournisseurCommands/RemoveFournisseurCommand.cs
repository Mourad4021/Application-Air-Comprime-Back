using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Commands.FournisseurCommands
{
    public class RemoveFournisseurCommand:IRequest<string>
    {
        public RemoveFournisseurCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
