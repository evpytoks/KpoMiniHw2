using System;
using Infrastructure.Interfacses;
using Aplication.Interfaces;

namespace Aplication.Services;

public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalsRepository Animals_;
    private readonly IEnclosuresRepository Enclosures_;
    private readonly IFeedingSchedulesRepository Schedules_;

    public ZooStatisticsService(IAnimalsRepository animals, IEnclosuresRepository enclosures, IFeedingSchedulesRepository schedules)
	{
        Animals_ = animals;
        Enclosures_ = enclosures;
        Schedules_ = schedules;
	}

    public async Task<int> GetAnimalNumberAsync()
    {
        var animals = await Animals_.GetAllAsync();
        return animals.Count();
    }

    public async Task<int> GetEnclosureNumberAsync()
    {
        var enclosures = await Enclosures_.GetAllAsync();
        return enclosures.Count();
    }

    public async Task<int> GetScheduleNumberAsync()
    {
        var schedules = await Schedules_.GetAllAsync();
        return schedules.Count();
    }
}

