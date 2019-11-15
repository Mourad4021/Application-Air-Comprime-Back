using MediatR;
using SuiviCompresseur.Notification.Domain.Commands;
using SuiviCompresseur.Notification.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.Notification.Domain.Handlers
{
    public class NotificationSeenHandler : IRequestHandler<NotificationSeenCommand, string>
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationSeenHandler(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }
        public Task<string> Handle(NotificationSeenCommand request, CancellationToken cancellationToken)
        {
            var result = notificationRepository.NotificationSeen(request.Id,request.Address);
            return Task.FromResult(result);
        }
    }
}
