using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Commands;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers
{
    public class CreateGenericHandler<T> : IRequestHandler<CreateGenericCommand<T>, string> where T : class
    {
        private readonly IGenericRepository<T> Repository;

        public CreateGenericHandler(IGenericRepository<T> Repository)
        {
            this.Repository = Repository;
        }

        public Task<string> Handle(CreateGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Post(request.Obj);
            return Task.FromResult(result);
        }
    }
}
