using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Queries.AttachementQuery
{
    public class GetAttachmentsByFournisseurIdQuery : IRequest<IEnumerable<AttachementFournisseur>>
    {
        public Guid FournisseurID { get; set; }
        public GetAttachmentsByFournisseurIdQuery(Guid fournisseurID)
        {
            FournisseurID = fournisseurID;
        }

    }
}
