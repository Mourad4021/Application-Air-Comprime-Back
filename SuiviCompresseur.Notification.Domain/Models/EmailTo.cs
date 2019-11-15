using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Models
{
    public class EmailTo
    {
        [Key]
        public Guid IdTo { get; set; }

        [ForeignKey("EmailFrom")]
        public Guid IdMail { get; set; }
        public string ToName { get; set; }
        public string ToAddresses { get; set; }

        public bool Seen { get; set; }

        public string ReceiveType { get; set; }

    }
}
