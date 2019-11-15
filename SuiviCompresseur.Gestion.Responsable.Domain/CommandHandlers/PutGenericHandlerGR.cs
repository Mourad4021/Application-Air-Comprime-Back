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
    public class PutGenericHandlerGR<T> : IRequestHandler<PutGenericCommandGR<T>, string> where T : class
    {
        private readonly IGenericRepositoryResponsable<T> Repository;
        public PutGenericHandlerGR(IGenericRepositoryResponsable<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<string> Handle(PutGenericCommandGR<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Put(request.Id, request.Obj);
            return Task.FromResult(result);
        }
    }
}
