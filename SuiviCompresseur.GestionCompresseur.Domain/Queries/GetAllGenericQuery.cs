using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Queries
{
    public class GetAllGenericQuery<T> : IRequest<IEnumerable<T>> where T : class
    {
    }


 
}
