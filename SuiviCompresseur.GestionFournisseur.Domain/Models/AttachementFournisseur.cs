using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Models
{
    public class AttachementFournisseur
    {
        [Key]
        public Guid AttachmentFournisseurId { get; set; }
        public string AttachmentNameF { get; set; }
        public string AttachmentFileFormatF { get; set; }
        public string AttachmentPhysicalPathF { get; set; }
        public string AttachmentOriginFileNameF { get; set; }
        public DateTime AttachementUploadDateF { get; set; } = DateTime.Now;

        //reference navigation properties
        public Guid FournisseurID { get; set; }
        public Fournisseur Fournisseur { get; set; }

    }
}
