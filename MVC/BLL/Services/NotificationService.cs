using BLL.Interfaces;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void SendNotification(string message, string userId)
        {
            var notification = new Notification
            {
                Message = message,
                DateCreated = DateTime.Now,
                IsRead = false,
                UserId = userId
            };
            _notificationRepository.AddNotification(notification);
        }
    }
}
