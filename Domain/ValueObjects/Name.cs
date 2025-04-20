using System;
namespace Domain.ValueObjects;

public class Name
{
    public string Name_ { get; }

    public Name(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя не может быть пустым.");
        }

        Name_ = name;
    }
}
