using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery
{
    public class GetAttachmentsByFicheSuiviIdQuery : IRequest<IEnumerable<Attachment>>
    {

        public Guid FichSuiviId { get; set; }
        public GetAttachmentsByFicheSuiviIdQuery(Guid ficheSuiviId)
        {
            FichSuiviId = ficheSuiviId;
        }

    }
}