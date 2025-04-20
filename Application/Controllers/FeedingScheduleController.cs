using System;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Interfacses;
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Presentation.DTOs;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedingScheduleController : ControllerBase
{
    private readonly IFeedingSchedulesRepository Schedules_;
    private readonly IFeedingOrganizationService FeedingOrganizationService_;

    public FeedingScheduleController(IFeedingSchedulesRepository schedules, IFeedingOrganizationService feedingOrganizationService)
	{
        Schedules_ = schedules;
        FeedingOrganizationService_ = feedingOrganizationService;
	}

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ScheduleDto>))]
    public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAll()
    {
        var schedules = await Schedules_.GetAllAsync();
        return Ok(schedules);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeedingSchedule))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FeedingSchedule>> GetById(Guid id)
    {
        var schedule = await Schedules_.GetByIdAsync(id);
        if (schedule == null)
        {
            return NotFound();
        }

        return Ok(schedule);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FeedingSchedule))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FeedingSchedule>> CreateSchedule(ScheduleDto dto)
    {
        try
        {
            var time = new EventTime(dto.FeedingTime);
            var food = new Food((Domain.Enums.FoodEnum)dto.Food);

            var schedule = new FeedingSchedule(dto.Animal, time, food);

            await FeedingOrganizationService_.AddScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetById), new { id = schedule.Id_ }, schedule);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedule(Guid id)
    {
        try
        {
            await FeedingOrganizationService_.DeleteScheduleAsync(id);
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

