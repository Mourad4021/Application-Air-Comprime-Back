using Microsoft.AspNetCore.Mvc;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Interfaces
{
    public interface IAttachementRepository
    {
        IEnumerable<Attachment> getAttachmentsByFicheSuiviId(Guid ficheSuiviId);
        IEnumerable<Attachment> getAttachmentsByEntretienReservoirId(Guid entretienReservoirID);
        byte[] getAttachmentFileById(Guid attachementId);


    }
}
