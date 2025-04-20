using System;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Enclosure
{
	public Guid Id_ { get; private set; }
	public AnimalFamily Type_ { get; private set; }
	public SizeCube3d Size_ { get; private set; }
	public Quantity QuantityOfAnimalsNow_ { get; private set; }
	public Quantity MaxQuantityOfAnimals { get; private set; }
	public bool IsClean { get; private set; }
	public List<Guid> Animals { get; }

	public Enclosure(AnimalFamily type, SizeCube3d size, Quantity maxQuantityOfAnimals)
	{
		Id_ = Guid.NewGuid();
		Type_ = type;
		Size_ = size;
		QuantityOfAnimalsNow_ = new Quantity(0);
		MaxQuantityOfAnimals = maxQuantityOfAnimals;
		IsClean = true;
		Animals = new List<Guid> { };
	}

	bool HasSpace()
	{
		return QuantityOfAnimalsNow_.Quantity_ < MaxQuantityOfAnimals.Quantity_;
	}

	bool IsSuitable(Animal animal)
	{
		return Type_.Family_ == Enums.AnimalFamilyEnum.Predator && (animal.Type_.AnimalType_ == Enums.AnimalTypeEnum.Lion || animal.Type_.AnimalType_ == Enums.AnimalTypeEnum.Wolf) ||
               Type_.Family_ == Enums.AnimalFamilyEnum.Herbivore && (animal.Type_.AnimalType_ == Enums.AnimalTypeEnum.Zebra || animal.Type_.AnimalType_ == Enums.AnimalTypeEnum.Girafee) ||
			   Type_.Family_ == Enums.AnimalFamilyEnum.Reptiles && (animal.Type_.AnimalType_ == Enums.AnimalTypeEnum.Crocodile) ||
			   Type_.Family_ == Enums.AnimalFamilyEnum.Birds && (animal.Type_.AnimalType_ == Enums.AnimalTypeEnum.Duck);
    }	

	public void AddAnimal(Animal animal)
	{
		if (Animals.Contains(animal.Id_))
		{
			return;
		}

        if (!IsSuitable(animal))
		{
            throw new InvalidOperationException("Нельзя это животное посадить в этот вольер.");
        }

        if (!HasSpace())
		{
			throw new InvalidOperationException("Вольер переполнен.");
		}

		QuantityOfAnimalsNow_ = new Quantity(QuantityOfAnimalsNow_.Quantity_ + 1);
		Animals.Add(animal.Id_);
	}

	public void RemoveAnimal(Animal animal)
	{
		Animals.Remove(animal.Id_);
        QuantityOfAnimalsNow_ = new Quantity(QuantityOfAnimalsNow_.Quantity_ - 1);
    }

	public void Clean()
	{
		IsClean = true;
	}

	public void BecomeDirty()
	{
		IsClean = false;
	}
}

