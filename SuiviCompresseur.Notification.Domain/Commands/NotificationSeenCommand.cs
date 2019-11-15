using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Commands
{
    public class NotificationSeenCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public NotificationSeenCommand(Guid id,string address)
        {
            Id = id;
            Address = address;
        }
    }
}
