using System;
using Domain.Entities;
using Infrastructure.Interfacses;

namespace Infrastructure.Repositories;

public class FeedingSchedulesRepository : IFeedingSchedulesRepository
{
    private readonly List<FeedingSchedule> FeedingSchedules_ = new();

    public Task<FeedingSchedule?> GetByIdAsync(Guid id)
    {
        var schedule = FeedingSchedules_.FirstOrDefault(s => s.Id_ == id);
        return Task.FromResult(schedule);
    }

    public Task<bool> AddAsync(FeedingSchedule schedule)
    {
        if (FeedingSchedules_.Any(s => s.Id_ == schedule.Id_))
        {
            return Task.FromResult(false);
        }

        FeedingSchedules_.Add(schedule);
        return Task.FromResult(true);
    }

    public Task<bool> RemoveAsync(Guid id)
    {
        var schedule = FeedingSchedules_.FirstOrDefault(s => s.Id_ == id);
        if (schedule is null)
        {
            return Task.FromResult(false);
        }

        FeedingSchedules_.Remove(schedule);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<FeedingSchedule>> GetAllAsync()
    {
        return Task.FromResult(FeedingSchedules_.AsEnumerable());
    }

    public Task<IEnumerable<FeedingSchedule>> GetAllByAnimalAsync(Guid animal)
    {
        var result = FeedingSchedules_
            .Where(schedule => schedule.Animal_ == animal);
        return Task.FromResult(result);
    }
}
