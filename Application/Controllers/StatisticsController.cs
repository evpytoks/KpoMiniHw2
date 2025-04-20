using System;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IZooStatisticsService ZooStatisticsService_;

    public StatisticsController(IZooStatisticsService zooStatisticsService)
	{
        ZooStatisticsService_ = zooStatisticsService;
	}

    [HttpGet("animals/number")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnimalsNumber()
    {
        var number = await ZooStatisticsService_.GetAnimalNumberAsync();
        return Ok(number);
    }

    [HttpGet("enclosure/number")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEnclosureNumber()
    {
        var number = await ZooStatisticsService_.GetEnclosureNumberAsync();
        return Ok(number);
    }

    [HttpGet("schedules/number")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSchedulesNumber()
    {
        var number = await ZooStatisticsService_.GetScheduleNumberAsync();
        return Ok(number);
    }
}

