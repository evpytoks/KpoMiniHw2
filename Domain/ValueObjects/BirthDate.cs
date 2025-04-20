using System;
using System.Globalization;

namespace Domain.ValueObjects;

public class BirthDate
{
    public DateTime Date_ { get; }

    public BirthDate(string date)
    {
        if (!DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            throw new ArgumentException("Неверный формат даты. Ожидается формат: дд.мм.гггг.");
        }

        Date_ = parsedDate;
    }
}
