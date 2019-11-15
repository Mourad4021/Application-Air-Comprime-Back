using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionCompresseur.Domain.CommandHandlers.AttachmentHandler
{
    public class GetAttachmentsByFicheSuiviIdHandler : IRequestHandler<GetAttachmentsByFicheSuiviIdQuery, IEnumerable<Attachment>>
    {
        
        public IAttachementRepository AttachementRepository;
        public GetAttachmentsByFicheSuiviIdHandler(IAttachementRepository attachementRepository)
        {
            AttachementRepository = attachementRepository;
        }

        Task<IEnumerable<Attachment>> IRequestHandler<GetAttachmentsByFicheSuiviIdQuery, IEnumerable<Attachment>>.Handle(GetAttachmentsByFicheSuiviIdQuery request, CancellationToken cancellationToken)
        {
            var attachements = AttachementRepository.getAttachmentsByFicheSuiviId(request.FichSuiviId);
            return Task.FromResult(attachements);
        }
    }
}
