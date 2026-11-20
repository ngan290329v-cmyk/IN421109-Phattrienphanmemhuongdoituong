using System;
using EduHub.Domain.Enums;
namespace EduHub.Domain.Entities
{
    public class Registration
    {
        //thuoc tinh
        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; }
        public Guid EventId { get; private set; }
        public DateTime RegisteredAt { get; private set; }
        public RegistrationStatus Status { get; private set; }
        // Constructor private
        private Registration(Guid memberId, Guid eventId)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            EventId = eventId;
            RegisteredAt = DateTime.UtcNow;
            Status = RegistrationStatus.Pending;
        }
        // Factory method tạo phiếu đăng ký
        public static Registration Create(Guid memberId, Guid eventId)
        {
            if (memberId == Guid.Empty)
            {
                throw new ArgumentException("MemberId không hợp lệ");
            }
            if (eventId == Guid.Empty)
            {
                throw new ArgumentException("EventId không hợp lệ");
            }
            return new Registration(memberId, eventId);
        }
        // Xác nhận phiếu đăng ký
        public void Confirm()
        {
            if (Status != RegistrationStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Chỉ phiếu Pending mới được xác nhận");
            }

            Status = RegistrationStatus.Confirmed;
        }
        // Hủy phiếu đăng ký
        public void Cancel()
        {
            if (Status == RegistrationStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "Phiếu đã hủy không thể thay đổi");
            }
            Status = RegistrationStatus.Cancelled;
        }
    }
}
