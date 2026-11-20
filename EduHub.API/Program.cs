using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Notifications;

var builder = WebApplication.CreateBuilder(args);

// 1. ĐĂNG KÝ SERVICES

builder.Services.AddControllers();

// Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. ĐĂNG KÝ REPOSITORY

builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
// 3. ĐĂNG KÝ NOTIFICATION SERVICES

builder.Services.AddScoped<EmailNotificationService>();
builder.Services.AddScoped<SmsNotificationService>();

// Đăng ký Notification Factory
builder.Services.AddScoped<INotificationFactory, NotificationFactory>();

// 4. ĐĂNG KÝ EVENT SERVICE
builder.Services.AddScoped<EventService>();
// 5. XÂY DỰNG ỨNG DỤNG
var app = builder.Build();
// 6. CẤU HÌNH SWAGGER
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
