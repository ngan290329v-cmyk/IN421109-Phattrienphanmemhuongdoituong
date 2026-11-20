using System;
using Xunit;
using EduHub.Domain.Entities;
using EduHub.Domain.Enums;
namespace EduHub.Tests
{
    public class RegistrationTests
    {
        //Tạo phiếu đăng ký có trạng thái Pending
        [Fact]
        public void Create_ShouldHavePendingStatus()
        {
            var memberId = Guid.NewGuid();
            var eventId = Guid.NewGuid();
            var registration = Registration.Create(memberId, eventId);
            Assert.Equal(
                RegistrationStatus.Pending,
                registration.Status
            );
        }
        //Xác nhận phiếu đăng ký
        [Fact]
        public void Confirm_ShouldChangeStatusToConfirmed()
        {
            var registration = Registration.Create(
                Guid.NewGuid(),
                Guid.NewGuid()
            );
            registration.Confirm();

            Assert.Equal(
                RegistrationStatus.Confirmed,
                registration.Status
            );
        }
        //Hủy rồi xác nhận lại phải báo lỗi
        [Fact]
        public void Confirm_AfterCancel_ShouldThrowException()
        {
            var registration = Registration.Create(
                Guid.NewGuid(),
                Guid.NewGuid()
            );
            registration.Cancel();
            Assert.Throws<InvalidOperationException>(
                () => registration.Confirm()
            );

            Assert.Equal(
                RegistrationStatus.Cancelled,
                registration.Status
            );
        }
        //Không cho tạo phiếu với MemberId rỗng
        [Fact]
        public void Create_WithEmptyMemberId_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                Registration.Create(Guid.Empty, Guid.NewGuid())
            );
        }
    }
}
