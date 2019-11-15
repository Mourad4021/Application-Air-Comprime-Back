using MediatR;
using SuiviCompresseur.Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Queries
{
    public class GetNotificationsQuery : IRequest<IEnumerable<EmailFrom>>
    {
        public GetNotificationsQuery(string address)
        {
            Address = address;
        }

        public string Address { get; set; }
    }
}
