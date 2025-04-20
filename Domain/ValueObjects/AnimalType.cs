using System;
using Domain.Enums;

namespace Domain.ValueObjects;

public class AnimalType
{
    public AnimalTypeEnum AnimalType_ { get; }

    public AnimalType(AnimalTypeEnum animalType)
    {
        if (!Enum.IsDefined(typeof(AnimalTypeEnum), animalType))
        {
            throw new ArgumentException("Нет такого животного.");
        }

        AnimalType_ = animalType;
    }
}
