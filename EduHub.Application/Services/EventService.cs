using System;
using System.Collections.Generic;
using EduHub.Domain.Entities;
using EduHub.Application.Interfaces;
namespace EduHub.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly INotificationFactory _notificationFactory;
        // Constructor Injection
        public EventService(
            IEventRepository eventRepository,
            INotificationFactory notificationFactory)
        {
            _eventRepository = eventRepository;
            _notificationFactory = notificationFactory;
        }
        // Tạo sự kiện mới
        public Event CreateEvent(
            Guid clubId,
            string title,
            DateTime date,
            int capacity,
            string channel)
        {
            var eventItem = Event.Create(
                clubId,
                title,
                date,
                capacity
            );
            // Lưu sự kiện
            _eventRepository.Add(eventItem);
            // Chọn kênh thông báo
            var notification = _notificationFactory.Create(channel);
            // Gửi thông báo
            notification.Send("Sự kiện mới: " + title);
            return eventItem;
        }
        // Lấy danh sách sự kiện theo câu lạc bộ
        public List<Event> GetEventsByClubId(Guid clubId)
        {
            return _eventRepository.GetByClubId(clubId);
        }
        // Lấy thông tin một sự kiện
        public Event? GetEventById(Guid id)
        {
            return _eventRepository.GetById(id);
        }
        // Xóa sự kiện
        public void DeleteEvent(Guid id)
        {
            _eventRepository.Delete(id);
        }
    }
}
