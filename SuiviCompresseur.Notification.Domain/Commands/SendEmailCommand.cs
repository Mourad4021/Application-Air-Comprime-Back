using MediatR;
using SuiviCompresseur.Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Commands
{
    public class SendEmailCommand : IRequest<string>
    {
        public EmailMessage EmailMessage { get; }


        public SendEmailCommand(EmailMessage emailMessage)
        {
            EmailMessage = emailMessage;
        }
    }
}
