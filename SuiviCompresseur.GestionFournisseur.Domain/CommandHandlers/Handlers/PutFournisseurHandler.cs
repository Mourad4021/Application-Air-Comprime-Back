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
    public class PutFournisseurHandler : IRequestHandler<PutFournisseurCommand, string>
    {
        private readonly IFournisseurRepository FournisseurRepository;
        public PutFournisseurHandler(IFournisseurRepository fournisseurRepository)
        {
            FournisseurRepository = fournisseurRepository;
        }
        public Task<string> Handle(PutFournisseurCommand request, CancellationToken cancellationToken)
        {
            var result = FournisseurRepository.PutFournisseurs(request.Id, request.Fournisseur);
            return Task.FromResult(result);
        }
    }
}
