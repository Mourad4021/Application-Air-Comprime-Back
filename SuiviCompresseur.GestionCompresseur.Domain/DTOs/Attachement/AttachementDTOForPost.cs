using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.DTOs
{
   public class AttachementDTOForPost
    { 

        public Guid AttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentFileFormat { get; set; }
        public string AttachmentPhysicalPath { get; set; }
        public DateTime AttachementUploadDate { get; set; } = DateTime.Now;

        public byte[] AttachementFile { get; set; }


        public Guid FicheSuiviID { get; set; }
      

    }
}
