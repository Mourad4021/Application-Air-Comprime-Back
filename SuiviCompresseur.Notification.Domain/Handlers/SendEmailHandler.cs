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
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, string>
    {
        private readonly INotificationRepository notificationRepository;

        public SendEmailHandler(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }
        public Task<string> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = notificationRepository.Send(request.EmailMessage);
            return Task.FromResult(result);
        }
    }
}
