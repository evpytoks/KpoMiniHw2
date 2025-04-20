using System;
using Domain.Enums;

namespace Domain.ValueObjects;

public class Food
{
    public FoodEnum Food_ { get; }

    public Food(FoodEnum food)
    {
        if (!Enum.IsDefined(typeof(FoodEnum), food))
        {
            throw new ArgumentException("Нет такого животного.");
        }

        Food_ = food;
    }
}
