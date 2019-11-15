using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Queries
{
    public class GetFournisseursQuery: IRequest<IEnumerable<Fournisseur>>
    {

    }
        
}
