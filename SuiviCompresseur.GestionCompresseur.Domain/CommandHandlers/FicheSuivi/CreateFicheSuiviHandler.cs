using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Commands.ficheSuivi;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers.FicheSuivi
{
    public class CreateFicheSuiviHandler : IRequestHandler<CreateFicheSuiviCommand, Guid>
    {
        private readonly IFiches_SuiviRepository Fiches_SuiviRepository;
        public CreateFicheSuiviHandler(IFiches_SuiviRepository fiches_SuiviRepository)
        {
            Fiches_SuiviRepository = fiches_SuiviRepository;
        }
        public Task<Guid> Handle(CreateFicheSuiviCommand request, CancellationToken cancellationToken)
        {
            var result = Fiches_SuiviRepository.AddFicheSuivi(request.FicheSuivi);
            return Task.FromResult(result);
        }
    }
}
   