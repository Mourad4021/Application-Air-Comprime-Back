using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Models
{
    public class EmailFrom
    {
        [Key]
        public Guid IdMail { get; set; }

        public string FromName { get; set; }
        public string FromAddresses { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public string ExceptionMessage { get; set; }
        public string MessageType { get; set; }

        public ICollection<EmailTo> EmailTos { get; set; }
    }
}
