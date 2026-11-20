using System;
using System.Collections.Generic;
using System.Linq;
using EduHub.Domain.Entities;
using EduHub.Application.Interfaces;
namespace EduHub.Infrastructure.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> events = new List<Event>();
        // Thêm sự kiện
        public void Add(Event eventItem)
        {
            events.Add(eventItem);
        }
        // Tìm sự kiện theo mã
        public Event? GetById(Guid id)
        {
            return events.FirstOrDefault(e => e.Id == id);
        }
        // Lấy danh sách sự kiện theo mã câu lạc bộ
        public List<Event> GetByClubId(Guid clubId)
        {
            return events.Where(e => e.ClubId == clubId).ToList();
        }
        // Xóa sự kiện
        public void Delete(Guid id)
        {
            var eventItem = GetById(id);
            if (eventItem != null)
            {
                events.Remove(eventItem);
            }
        }
    }
}
