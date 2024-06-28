using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotificationsForUser(int userId);
        void AddNotification(Notification notification);
        void MarkAsRead(int notificationId);
    }
}
