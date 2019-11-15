using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Commands.FournisseurCommands;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionFournisseur.Domain.CommandHandlers.Handlers
{
    public class RemoveFournisseurHandler: IRequestHandler<RemoveFournisseurCommand, string>
    {
        private readonly IFournisseurRepository FournisseurRepository;
        public RemoveFournisseurHandler(IFournisseurRepository fournisseurRepository)
        {
            FournisseurRepository = fournisseurRepository;
        }

        public Task<string> Handle(RemoveFournisseurCommand request, CancellationToken cancellationToken)
        {
            var result = FournisseurRepository.DeleteFournisseurs(request.Id);
            return Task.FromResult(result);
        }
    }
}
