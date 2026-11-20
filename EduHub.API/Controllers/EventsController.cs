using System;
using EduHub.Application.Services;
using Microsoft.AspNetCore.Mvc;
namespace EduHub.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;
        // Constructor Injection
        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }
        // 1. GET: api/clubs/{clubId}/events
        // Lấy danh sách sự kiện của câu lạc bộ
        [HttpGet("clubs/{clubId:guid}/events")]
        [ProducesResponseType(200)]
        public IActionResult GetByClubId(Guid clubId)
        {
            var events = _eventService.GetEventsByClubId(clubId);
            return Ok(events);
        }
        // 2. GET: api/events/{id}
        // Lấy chi tiết một sự kiện
        [HttpGet("events/{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(Guid id)
        {
            var eventItem = _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sự kiện."
                });
            }
            return Ok(eventItem);
        }
        // 3. POST: api/clubs/{clubId}/events
        // Tạo sự kiện mới
        [HttpPost("clubs/{clubId:guid}/events")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(
            Guid clubId,
            [FromBody] CreateEventRequest request)
        {
            try
            {
                var eventItem = _eventService.CreateEvent(
                    clubId,
                    request.Title,
                    request.Date,
                    request.Capacity,
                    request.Channel
                );
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = eventItem.Id },
                    eventItem
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        // 4. DELETE: api/events/{id}
        // Xóa sự kiện
        [HttpDelete("events/{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            var eventItem = _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sự kiện cần xóa."
                });
            }
            _eventService.DeleteEvent(id);

            return NoContent();
        }
    }
    // Dữ liệu nhận khi tạo sự kiện
    public record CreateEventRequest(
        string Title,
        DateTime Date,
        int Capacity,
        string Channel = "email"
    );
}
