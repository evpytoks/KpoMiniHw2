using System;
using System.Globalization;

namespace Domain.ValueObjects;

public class EventTime
{
    public TimeOnly Time_ { get; }

    public EventTime(string time)
    {
        if (!TimeOnly.TryParseExact(time.Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime))
        {
            throw new ArgumentException("Неверный формат времени. Ожидается формат: ЧЧ:мм (например, 14:30).");
        }

        Time_ = parsedTime;
    }
}
