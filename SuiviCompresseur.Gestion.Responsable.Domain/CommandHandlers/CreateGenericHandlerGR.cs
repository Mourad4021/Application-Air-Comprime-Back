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
    public class CreateGenericHandlerGR<T> : IRequestHandler<CreateGenericCommandGR<T>, string> where T : class
    {
        private readonly IGenericRepositoryResponsable<T> Repository;
        public CreateGenericHandlerGR(IGenericRepositoryResponsable<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<string> Handle(CreateGenericCommandGR<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Post(request.Obj);
            return Task.FromResult(result);
        }
    }
}
