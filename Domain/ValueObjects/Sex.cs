using System;
using Domain.Enums;

namespace Domain.ValueObjects;

public class Sex
{
    public SexEnum Sex_ { get; }

    public Sex(SexEnum sex)
    {
        if (!Enum.IsDefined(typeof(SexEnum), sex))
        {
            throw new ArgumentException("Нет такого животного.");
        }

        Sex_ = sex;
    }
}
