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
    public class GetGenericHandlerGR<T> : IRequestHandler<GetGenericQueryGR<T>, T> where T : class
    {
        private readonly IGenericRepositoryResponsable<T> Repository;
        public GetGenericHandlerGR(IGenericRepositoryResponsable<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<T> Handle(GetGenericQueryGR<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Get(request.Id);
            return Task.FromResult(result);
        }
    }
}
