using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Queries.AttachementQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionFournisseur.Domain.CommandHandlers.AttachementFournisseurHandler
{
    public class GetAttachmentFournisseurFileByIdHandler : IRequestHandler<GetAttachmentFournisseurFileByIdQuery, byte[]>
    {
        public IAttachementFournisseurRepository AttachementFournisseurRepository;
    public GetAttachmentFournisseurFileByIdHandler(IAttachementFournisseurRepository attachementFournisseurRepository)
    {
            AttachementFournisseurRepository = attachementFournisseurRepository;
    }
    public Task<byte[]> Handle(GetAttachmentFournisseurFileByIdQuery request, CancellationToken cancellationToken)
    {
        var attachementFile = AttachementFournisseurRepository.getAttachmentFileById(request.AttachementFournisseurId);
        return Task.FromResult(attachementFile);

    }
}
}
