using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Commands;
using SuiviCompresseur.GestionFournisseur.Domain.Commands.FournisseurCommands;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionFournisseur.Domain.CommandHandlers.Handlers
{
    public class AddFournisseurHandler : IRequestHandler<AddFournisseurCommand, string>
    {
        private readonly IFournisseurRepository FournisseurRepository;
        public AddFournisseurHandler(IFournisseurRepository fournisseurRepository)
        {
            FournisseurRepository = fournisseurRepository;
        }
        public Task<string> Handle(AddFournisseurCommand request, CancellationToken cancellationToken)
        {
            var result = FournisseurRepository.AddF(request.Fournisseur);
            return Task.FromResult(result);
        }
    }
}
