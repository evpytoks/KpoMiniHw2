using System;
using Domain.Enums;

namespace Domain.ValueObjects;

public class AnimalFamily
{
    public AnimalFamilyEnum Family_ { get; }

    public AnimalFamily(AnimalFamilyEnum family)
    {
        if (!Enum.IsDefined(typeof(AnimalFamilyEnum), family))
        {
            throw new ArgumentException("Нет такого семейства животных.");
        }

        Family_ = family;
    }
}
