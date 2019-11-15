using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Gestion.Responsable.Domain.Queries
{
    public class GetAllGenericQueryGR<T> :IRequest<IEnumerable<T>> where T : class
    {
    }
}
