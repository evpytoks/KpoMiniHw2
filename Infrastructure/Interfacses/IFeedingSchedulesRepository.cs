using System;
using Domain.Entities;

namespace Infrastructure.Interfacses;

public interface IFeedingSchedulesRepository
{
    Task<FeedingSchedule?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(FeedingSchedule feedingSchedule);
    Task<bool> RemoveAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> GetAllAsync();
    public Task<IEnumerable<FeedingSchedule>> GetAllByAnimalAsync(Guid animal);
}
