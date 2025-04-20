using System;
namespace Presentation.DTOs;

public class ScheduleDto
{
    public required Guid Animal { get; set; }
    public required string FeedingTime { get; set; }
    public required int Food { get; set; }
}

