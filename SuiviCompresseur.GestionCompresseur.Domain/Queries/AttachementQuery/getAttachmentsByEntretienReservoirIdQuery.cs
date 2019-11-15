using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery
{
    public class GetAttachmentsByEntretienReservoirIdQuery : IRequest<IEnumerable<Attachment>>
    {
        public Guid EntretienReservoirID { get; set; }
        public GetAttachmentsByEntretienReservoirIdQuery(Guid entretienReservoirID)
        {
            EntretienReservoirID = entretienReservoirID;
        }
    }
}
