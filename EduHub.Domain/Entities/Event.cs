using System;
namespace EduHub.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; private set; }
        public Guid ClubId { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public int Capacity { get; private set; }
        private Event(Guid clubId, string title, DateTime date, int capacity)
        {
            Id = Guid.NewGuid();
            ClubId = clubId;
            Title = title;
            Date = date;
            Capacity = capacity;
        }
        public static Event Create(
            Guid clubId,
            string title,
            DateTime date,
            int capacity)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title không được để trống");
            if (capacity <= 0)
                throw new ArgumentException("Capacity phải lớn hơn 0");
            return new Event(clubId, title, date, capacity);
        }
    }
}
