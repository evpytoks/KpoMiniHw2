using System;
namespace Aplication.Interfaces;

public interface IZooStatisticsService
{
    Task<int> GetAnimalNumberAsync();
    Task<int> GetEnclosureNumberAsync();
    Task<int> GetScheduleNumberAsync();
}
