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
    public class GetFournisseursHandlers : IRequestHandler<GetFournisseursQuery, IEnumerable<Fournisseur>>
    {
        private readonly IFournisseurRepository FournisseurRepository ;

        public GetFournisseursHandlers(IFournisseurRepository fournisseurRepository)
        {
            FournisseurRepository = fournisseurRepository;
        }
        public Task<IEnumerable<Fournisseur>> Handle(GetFournisseursQuery request, CancellationToken cancellationToken)
        {
            var Fournisseur = FournisseurRepository.GetFournisseurs();

            return Task.FromResult(Fournisseur);
        }
    }
}
