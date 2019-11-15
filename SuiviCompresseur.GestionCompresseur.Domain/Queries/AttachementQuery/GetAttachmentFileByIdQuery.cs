using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery
{
    public class GetAttachmentFileByIdQuery:IRequest<byte[]>
    {
         public Guid AttachementId { get; set; }
        public GetAttachmentFileByIdQuery(Guid attachementId)
        {
            AttachementId = attachementId;
        }
    }
}
