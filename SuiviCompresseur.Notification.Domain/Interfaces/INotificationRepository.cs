using SuiviCompresseur.Notification.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.Notification.Domain.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<EmailFrom> GetNotifications(string address);
        string Send(EmailMessage emailMessage);
        string NotificationSeen(Guid Id, string address);
    }
}
