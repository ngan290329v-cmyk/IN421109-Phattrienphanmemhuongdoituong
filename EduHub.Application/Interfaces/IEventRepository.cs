using System;
using System.Collections.Generic;
using EduHub.Domain.Entities;
namespace EduHub.Application.Interfaces
{
    public interface IEventRepository
    {
        void Add(Event eventItem);
        Event? GetById(Guid id);
        List<Event> GetByClubId(Guid clubId);
        void Delete(Guid id);
    }
}
