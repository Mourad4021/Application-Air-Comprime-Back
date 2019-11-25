using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Models
{
    public class Attachment
    {
        //scalar properties 
        public Guid AttachmentId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentFileFormat { get; set; }
        public string AttachmentPhysicalPath{ get; set; }
        public string AttachmentOriginFileName { get; set; }
        public DateTime AttachementUploadDate { get; set; } = DateTime.Now;
        
        //reference navigation properties
        public Guid? FicheSuiviID { get; set; }
        public Fiche_Suivi ficheSuivi { get; set; }

        public Guid? EntretienReservoirID { get; set; }
        public EntretienReservoir EntretienReservoir { get; set; }


        //collection navigation properties
    }
}
