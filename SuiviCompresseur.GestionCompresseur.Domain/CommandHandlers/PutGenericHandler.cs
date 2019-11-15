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
    public class PutGenericHandler<T> : IRequestHandler<PutGenericCommand<T>, string> where T : class
    {
        private readonly IGenericRepository<T> Repository;
        public PutGenericHandler(IGenericRepository<T> Repository)
        {
            this.Repository = Repository;
        }
        public Task<string> Handle(PutGenericCommand<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Put(request.Id, request.Obj);
            return Task.FromResult(result);
        }
    }
}
