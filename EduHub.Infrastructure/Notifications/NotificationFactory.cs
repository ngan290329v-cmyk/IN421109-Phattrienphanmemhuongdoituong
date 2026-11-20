using System;
using EduHub.Application.Interfaces;
namespace EduHub.Infrastructure.Notifications
{
    public class NotificationFactory : INotificationFactory
    {
        public INotificationService Create(string channel)
        {
            if (channel.ToLower() == "email")
            {
                return new EmailNotificationService();
            }
            if (channel.ToLower() == "sms")
            {
                return new SmsNotificationService();
            }
            throw new ArgumentException("Kênh thông báo không hợp lệ");
        }
    }
}
