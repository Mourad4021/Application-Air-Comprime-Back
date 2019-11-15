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
    public class RemoveGenericHandler<T> : IRequestHandler<RemoveGenericCommand<T>, string> where T : class
    {
        private readonly IGenericRepository<T> Repository;
        public RemoveGenericHandler(IGenericRepository<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<string> Handle(Commands.RemoveGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Delete(request.Id);
            return Task.FromResult(result);
        }
    }
}
