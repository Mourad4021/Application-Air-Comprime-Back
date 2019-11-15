using MediatR;
using SuiviCompresseur.Notification.Domain.Interfaces;
using SuiviCompresseur.Notification.Domain.Models;
using SuiviCompresseur.Notification.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.Notification.Domain.Handlers
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationsQuery, IEnumerable<EmailFrom>>
    {
        private readonly INotificationRepository notificationRepository;
        public GetNotificationHandler(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }
        public Task<IEnumerable<EmailFrom>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var result = notificationRepository.GetNotifications(request.Address);
            return Task.FromResult(result);
        }
    }
}
