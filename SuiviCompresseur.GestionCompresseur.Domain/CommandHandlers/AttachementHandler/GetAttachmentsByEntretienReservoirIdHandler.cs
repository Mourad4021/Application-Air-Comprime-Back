using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers.AttachementHandler
{
    public class GetAttachmentsByEntretienReservoirIdHandler : IRequestHandler<GetAttachmentsByEntretienReservoirIdQuery, IEnumerable<Attachment>>
    {
        public IAttachementRepository AttachementRepository;
        public GetAttachmentsByEntretienReservoirIdHandler(IAttachementRepository attachementRepository)
        {
            AttachementRepository = attachementRepository;
        }
        public Task<IEnumerable<Attachment>> Handle(GetAttachmentsByEntretienReservoirIdQuery request, CancellationToken cancellationToken)
        {
            var attachements = AttachementRepository.getAttachmentsByEntretienReservoirId(request.EntretienReservoirID);
            return Task.FromResult(attachements);
        }
    }
}
