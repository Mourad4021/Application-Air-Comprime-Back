using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Interfaces
{
    public interface IAttachementFournisseurRepository
    {
        IEnumerable<AttachementFournisseur> getAttachmentsByFournisseurId(Guid fournisseurID);
        byte[] getAttachmentFileById(Guid attachementId);
    }
}
