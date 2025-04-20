using System;
namespace Domain.ValueObjects;

public class Quantity
{
    public int Quantity_ { get; }

    public Quantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Количество не может быть отрицательным.");
        }

        Quantity_ = quantity;
    }
}
