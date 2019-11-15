using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Models
{
    public class EmailMessage
    {
        public string ToAddresses { get; set; }
        public string CcAddresses { get; set; }
        public string CccAddresses { get; set; }
        public string FromAddresses { get; set; }
        public string Files { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
