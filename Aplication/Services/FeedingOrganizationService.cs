using System;
using Infrastructure.Interfacses;
using Aplication.Interfaces;
using MediatR;
using Domain.Events;
using Domain.Entities;

namespace Aplication.Services;

public class FeedingOrganizationService : IFeedingOrganizationService
{
	private readonly IFeedingSchedulesRepository Schedules_;
    private readonly IAnimalsRepository Animals_;
    private readonly IMediator Mediator_;

    public FeedingOrganizationService(IAnimalsRepository animals, IFeedingSchedulesRepository schedules, IMediator mediator)
	{
		Schedules_ = schedules;
		Animals_ = animals;
        Mediator_ = mediator;
    }

    public async Task Feed(Guid scheduleId)
	{
		var schedule = await Schedules_.GetByIdAsync(scheduleId) ?? throw new ArgumentException("Нет такого кормления в расписании");

		var animalId = schedule.Animal_;
		var animal = await Animals_.GetByIdAsync(animalId) ?? throw new ArgumentException("Нет такого животного");

		animal.Feed();
		schedule.Complete();

        var @event = new FeedingTimeEvent(animal.Id_, schedule.Food_, animal.EnclosureId_);
        await Mediator_.Publish(@event);
    }

    public async Task AddScheduleAsync(FeedingSchedule schedule)
    {
        var animal = await Animals_.GetByIdAsync(schedule.Animal_) ?? throw new ArgumentException("Нет такого животного в зоопарке");

        var result = await Schedules_.AddAsync(schedule);
        if (!result)
        {
            throw new ArgumentException("Это расписание уже числится в зоопарке.");
        }
    }

    public async Task DeleteScheduleAsync(Guid scheduleId)
    {
        await Schedules_.RemoveAsync(scheduleId);
    }
}

