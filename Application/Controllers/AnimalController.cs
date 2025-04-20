using System;
using Microsoft.AspNetCore.Mvc;
using Aplication.Interfaces;
using Infrastructure.Interfacses;
using Presentation.DTOs;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Repositories;
using Aplication.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalsRepository Animals_;
    private readonly IAnimalTransferSetvice AnimalTransferService_;

    public AnimalController(IAnimalsRepository animals, IAnimalTransferSetvice animalTransferSetvice)
	{
        Animals_ = animals;
        AnimalTransferService_ = animalTransferSetvice;
	}

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnimalDto>))]
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAll()
    {
        var animals = await Animals_.GetAllAsync();
        return Ok(animals);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Animal))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Animal>> GetById(Guid id)
    {
        var animal = await Animals_.GetByIdAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        return Ok(animal);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Animal))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Animal>> CreateAnimal(AnimalDto dto)
    {
        try
        {
            var animalType = new AnimalType((Domain.Enums.AnimalTypeEnum)dto.AnimalType);
            var name = new Name(dto.Name);
            var birthday = new BirthDate(dto.BirthDate);
            var sex = new Sex((Domain.Enums.SexEnum)dto.Sex);
            var favoriteFood = new Food((Domain.Enums.FoodEnum)dto.FavouriteFood);
            var status = new Status((Domain.Enums.StatusEnum)dto.Status);

            var animal = new Animal(animalType, name, birthday, sex, favoriteFood, status);

            await AnimalTransferService_.AddAnimalAsync(animal, dto.Enclosure);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id_ }, animal);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAnimal(Guid id)
    {
        try
        {
            await AnimalTransferService_.DeleteAnimalAsync(id);
            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPatch("transfer")]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> TransferAnimalToEnclosure([FromBody] TransferAnimalDto dto)
    {
        try
        {
            await AnimalTransferService_.MoveAnimalAsync(dto.Animal, dto.Enclosure);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

