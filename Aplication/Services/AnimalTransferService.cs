using System;
using Infrastructure.Interfacses;
using Domain.Events;
using Domain.Entities;
using Aplication.Interfaces;
using MediatR;


namespace Aplication.Services;

public class AnimalTransferService : IAnimalTransferSetvice
{
	private readonly IAnimalsRepository Animals_;
	private readonly IEnclosuresRepository Enclosures_;
    private readonly IFeedingSchedulesRepository Schedules_;
    private readonly IMediator Mediator_;

	public AnimalTransferService(IAnimalsRepository animals, IEnclosuresRepository enclosures, IFeedingSchedulesRepository schedules, IMediator mediator)
	{
		Animals_ = animals;
		Enclosures_ = enclosures;
		Schedules_ = schedules;
		Mediator_ = mediator;
	}

	public async Task AddAnimalAsync(Animal animal, Guid enclosureId)
	{
        var enclosure = await Enclosures_.GetByIdAsync(enclosureId) ?? throw new ArgumentException("Нет такого вольера.");

        var result = await Animals_.AddAsync(animal);
		if (!result)
		{
            throw new ArgumentException("Это животное уже числится в зоопарке.");
        }

		enclosure.AddAnimal(animal);
		animal.MoveToEnclosure(enclosure.Id_);
	}

    public async Task MoveAnimalAsync(Guid animalId, Guid toEnclosureId)
	{
		var animal = await Animals_.GetByIdAsync(animalId) ?? throw new ArgumentException("Нет такого животного.");

		var fromEnclosureId = animal.EnclosureId_;
		if (fromEnclosureId == Guid.Empty)
		{
			throw new ArgumentException("Животное не находилось в вольере.");
		}

		var fromEnclosure = await Enclosures_.GetByIdAsync(fromEnclosureId) ?? throw new ArgumentException("Нет такого вольера.");
		fromEnclosure.RemoveAnimal(animal);

		var toEnclosure = await Enclosures_.GetByIdAsync(toEnclosureId) ?? throw new ArgumentException("Нет такого вольера.");
		toEnclosure.AddAnimal(animal);
        animal.MoveToEnclosure(toEnclosureId);

        var @event = new AnimalMovedEvent(animal.Id_, fromEnclosure.Id_, toEnclosure.Id_);
        await Mediator_.Publish(@event);
    }

	public async Task DeleteAnimalAsync(Guid animalId)
	{
        var animal = await Animals_.GetByIdAsync(animalId) ?? throw new ArgumentException("Нет такого животного.");

		var fromEnclosureId = animal.EnclosureId_;
		if (fromEnclosureId != Guid.Empty)
		{
            var fromEnclosure = await Enclosures_.GetByIdAsync(fromEnclosureId) ?? throw new ArgumentException("Нет такого вольера.");
            fromEnclosure.RemoveAnimal(animal);
        }

        var schedulesToDelete = await Schedules_.GetAllByAnimalAsync(animalId);
		if (schedulesToDelete != null)
		{
            foreach (var schedule in schedulesToDelete)
            {
                await Schedules_.RemoveAsync(schedule.Id_);
            }
        }

        await Animals_.RemoveAsync(animalId);
    }

	public async Task AddEnclosureAsync(Enclosure enclosure)
	{
        var result = await Enclosures_.AddAsync(enclosure);
        if (!result)
        {
            throw new ArgumentException("Этот вольер уже числится в зоопарке.");
        }
    }

	public async Task DeleteEnclosureAsync(Guid enclosureId)
	{
		var enclosure = await Enclosures_.GetByIdAsync(enclosureId) ?? throw new ArgumentException("Нет такого вольера.");

		if (enclosure.QuantityOfAnimalsNow_.Quantity_ != 0)
		{
			throw new ArgumentException("Нельзя удалять не пустой вольер.");
        }

		await Enclosures_.RemoveAsync(enclosureId);
	}
}

