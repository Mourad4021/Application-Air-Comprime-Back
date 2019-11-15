using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers
{
    public class GetGenericHandler<T> : IRequestHandler<GetGenericQuery<T>, T> where T: class
    {
        private readonly IGenericRepository<T> Repository;
        public GetGenericHandler(IGenericRepository<T> Repository)
        {
            this.Repository = Repository;
        }

        public Task<T> Handle(GetGenericQuery<T> request, CancellationToken cancellationToken)
        {
            var result = Repository.Get(request.Id);
            return Task.FromResult(result);
        }
    }
    
}
