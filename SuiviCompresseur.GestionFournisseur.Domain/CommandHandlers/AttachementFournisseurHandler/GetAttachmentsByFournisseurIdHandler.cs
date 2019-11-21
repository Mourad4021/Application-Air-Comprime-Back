using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using SuiviCompresseur.GestionFournisseur.Domain.Queries.AttachementQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionFournisseur.Domain.CommandHandlers.AttachementFournisseurHandler
{
    public class GetAttachmentsByFournisseurIdHandler : IRequestHandler<GetAttachmentsByFournisseurIdQuery, IEnumerable<AttachementFournisseur>>
    {

        public IAttachementFournisseurRepository AttachementFournisseurRepository;
        public GetAttachmentsByFournisseurIdHandler(IAttachementFournisseurRepository attachementFournisseurRepository)
        {
            AttachementFournisseurRepository = attachementFournisseurRepository;
        }

         Task<IEnumerable<AttachementFournisseur>> IRequestHandler<GetAttachmentsByFournisseurIdQuery, IEnumerable<AttachementFournisseur>>.Handle(GetAttachmentsByFournisseurIdQuery request, CancellationToken cancellationToken)
        {
            var attachements = AttachementFournisseurRepository.getAttachmentsByFournisseurId(request.FournisseurID);
            return Task.FromResult(attachements);
        }

        
    }
}
