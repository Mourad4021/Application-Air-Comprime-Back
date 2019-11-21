using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Queries.AttachementQuery
{
    public class GetAttachmentFournisseurFileByIdQuery : IRequest<byte[]>
    {
        public Guid AttachementFournisseurId { get; set; }
        public GetAttachmentFournisseurFileByIdQuery(Guid attachementFournisseurId)
        {
            AttachementFournisseurId = attachementFournisseurId;
        }
    }
    
}
