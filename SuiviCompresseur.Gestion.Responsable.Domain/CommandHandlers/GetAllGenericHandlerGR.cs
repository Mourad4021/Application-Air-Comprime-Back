using MediatR;
using SuiviCompresseur.Gestion.Responsable.Domain.Interfaces;
using SuiviCompresseur.Gestion.Responsable.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.Gestion.Responsable.Domain.CommandHandlers
{
    public class GetAllGenericHandlerGR<T> : IRequestHandler<GetAllGenericQueryGR<T>, IEnumerable<T>> where T : class
    {
        private readonly IGenericRepositoryResponsable<T> Repository;
        public GetAllGenericHandlerGR(IGenericRepositoryResponsable<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<IEnumerable<T>> Handle(GetAllGenericQueryGR<T> request, CancellationToken cancellationToken)
        {
            var Utilisateur = Repository.GetAll();

            return Task.FromResult(Utilisateur);
        }
    }
}
