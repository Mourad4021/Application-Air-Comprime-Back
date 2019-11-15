using MediatR;
using SuiviCompresseur.Gestion.Responsable.Domain.Commands;
using SuiviCompresseur.Gestion.Responsable.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.Gestion.Responsable.Domain.CommandHandlers
{
    public class RemoveGenericHandlerGR<T> : IRequestHandler<RemoveGenericCommandGR<T>, string> where T : class
    {
        private readonly IGenericRepositoryResponsable<T> Repository;
        public RemoveGenericHandlerGR(IGenericRepositoryResponsable<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<string> Handle(RemoveGenericCommandGR<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Delete(request.Id);
            return Task.FromResult(result);
        }
    }
}
