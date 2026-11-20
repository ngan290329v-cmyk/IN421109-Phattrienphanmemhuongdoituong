using System;
using Xunit;
using Moq;
using EduHub.Domain.Entities;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
namespace EduHub.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void CreateEvent_ValidData_ShouldAddAndSendOnce()
        {
            // Arrange: Tạo các đối tượng giả lập
            var repositoryMock = new Mock<IEventRepository>();
            var notificationMock =
                new Mock<INotificationService>();
            var factoryMock =
                new Mock<INotificationFactory>();
            factoryMock
                .Setup(f => f.Create("email"))
                .Returns(notificationMock.Object);

            var service = new EventService(
                repositoryMock.Object,
                factoryMock.Object
            );
            // Act: Thực hiện tạo sự kiện
            service.CreateEvent(
                Guid.NewGuid(),
                "Hoi thao CNTT",
                DateTime.Now,
                100,
                "email"
            );
            // Assert: Kiểm tra kết quả
            repositoryMock.Verify(
                r => r.Add(It.IsAny<Event>()),
                Times.Once
            );
            notificationMock.Verify(
                n => n.Send("Sự kiện mới: Hoi thao CNTT"),
                Times.Once
            );
        }
    }
}
