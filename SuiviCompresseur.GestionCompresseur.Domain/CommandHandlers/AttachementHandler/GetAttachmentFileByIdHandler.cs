using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers.AttachementHandler
{
    public class GetAttachmentFileByIdHandler : IRequestHandler<GetAttachmentFileByIdQuery, byte[]>
    {
        public IAttachementRepository AttachementRepository;
        public GetAttachmentFileByIdHandler(IAttachementRepository attachementRepository)
        {
            AttachementRepository = attachementRepository;
        }
        public Task<byte[]> Handle(GetAttachmentFileByIdQuery request, CancellationToken cancellationToken)
        {
            var attachementFile = AttachementRepository.getAttachmentFileById(request.AttachementId);
            return Task.FromResult(attachementFile);

        }
    }
}
