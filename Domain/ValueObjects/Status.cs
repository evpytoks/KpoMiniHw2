using System;
using Domain.Enums;

namespace Domain.ValueObjects;

public class Status
{
    public StatusEnum Status_ { get; }

    public Status(StatusEnum status)
    {
        if (!Enum.IsDefined(typeof(StatusEnum), status))
        {
            throw new ArgumentException("Нет такого животного.");
        }

        Status_ = status;
    }
}
