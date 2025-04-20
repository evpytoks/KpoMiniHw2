using System;
using Domain.Entities;

namespace Aplication.Interfaces;

public interface IFeedingOrganizationService
{
    Task Feed(Guid schedule);
    Task AddScheduleAsync(FeedingSchedule schedule);
    Task DeleteScheduleAsync(Guid scheduleId);
}
