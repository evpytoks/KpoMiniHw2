using System;
using Aplication.Interfaces;
using Aplication.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Interfacses;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosureController : ControllerBase
{
    private readonly IEnclosuresRepository Enclosures_;
    private readonly IAnimalTransferSetvice AnimalTransferService_;

    public EnclosureController(IEnclosuresRepository enclosures, IAnimalTransferSetvice animalTransferSetvice)
	{
        Enclosures_ = enclosures;
        AnimalTransferService_ = animalTransferSetvice;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EnclosureDto>))]
    public async Task<ActionResult<IEnumerable<EnclosureDto>>> GetAll()
    {
        var enclosures = await Enclosures_.GetAllAsync();
        return Ok(enclosures);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Enclosure))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Enclosure>> GetById(Guid id)
    {
        var enclosure = await Enclosures_.GetByIdAsync(id);
        if (enclosure == null)
        {
            return NotFound();
        }

        return Ok(enclosure);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Enclosure))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Enclosure>> CreateAnimal(EnclosureDto dto)
    {
        try
        {
            var type = new AnimalFamily((Domain.Enums.AnimalFamilyEnum)dto.Type);
            var size = new SizeCube3d(dto.Length, dto.Width, dto.Heigth);
            var quantity = new Quantity(dto.Quantity);

            var enclosure = new Enclosure(type, size, quantity);

            await AnimalTransferService_.AddEnclosureAsync(enclosure);
            return CreatedAtAction(nameof(GetById), new { id = enclosure.Id_ }, enclosure);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEnclosure(Guid id)
    {
        try
        {
            await AnimalTransferService_.DeleteEnclosureAsync(id);
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}

