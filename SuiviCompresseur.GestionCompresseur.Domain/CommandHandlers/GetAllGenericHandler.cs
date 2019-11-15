using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SuiviCompresseur.GestionCompresseur.Domain.Models;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers
{
    public class GetAllGenericHandler<T> : IRequestHandler<GetAllGenericQuery<T>, IEnumerable<T>> where T: class
    {
        private readonly IGenericRepository<T> Repository;
        public GetAllGenericHandler(IGenericRepository<T> Repository)
        {
            this.Repository = Repository;
        }

        public Task<IEnumerable<T>> Handle(GetAllGenericQuery<T> request, CancellationToken cancellationToken)
        {
           
            var result = Repository.GetAll();
            return Task.FromResult(result);
        }


    }

    
}
