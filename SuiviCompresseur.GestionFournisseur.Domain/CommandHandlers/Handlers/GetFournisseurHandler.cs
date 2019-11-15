using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using SuiviCompresseur.GestionFournisseur.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionFournisseur.Domain.CommandHandlers.Handlers
{
    public class GetFournisseurHandler: IRequestHandler<GetFournisseurQuery, Fournisseur>
    {
        private readonly IFournisseurRepository FournisseurRepository ;
        public GetFournisseurHandler(IFournisseurRepository fournisseurRepository)
        {
            FournisseurRepository = fournisseurRepository;
        }
        public Task<Fournisseur> Handle(GetFournisseurQuery request, CancellationToken cancellationToken)
        {
            var result = FournisseurRepository.GetFournisseur(request.Id);

            return Task.FromResult(result);
        }
    }
}
