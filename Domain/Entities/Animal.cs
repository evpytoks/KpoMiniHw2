using System;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Animal
{
    public Guid Id_ { get; private set; }
    public AnimalType Type_ { get; private set; }
    public Name Name_ { get; private set; }
    public BirthDate Birthday_ { get; private set; }
    public Sex Sex_ { get; private set; }
    public Food FavouriteFood_ { get; private set; }
    public Status Status_ { get; private set; }
    public bool IsHungry_ { get; private set; }
    public Guid EnclosureId_ { get; private set; }

    public Animal(AnimalType type, Name name, BirthDate birthday, Sex sex, Food favouriteFood, Status status)
	{
        Id_ = Guid.NewGuid();
        Type_ = type;
        Name_ = name;
        Birthday_ = birthday;
        Sex_ = sex;
        FavouriteFood_ = favouriteFood;
        Status_ = status;
        IsHungry_ = true;
        EnclosureId_ = Guid.Empty;
    }

    public void Feed()
    {
        IsHungry_ = false;
    }

    public void BecomeHungry()
    {
        IsHungry_ = true;
    }

    public void Treat()
    {
        Status_ = new Status(Enums.StatusEnum.Healthy);
    }

    public void FellIll()
    {
        Status_ = new Status(Enums.StatusEnum.Ill);
    }

    public void MoveToEnclosure(Guid enclosure)
    {
        EnclosureId_ = enclosure;
    }
}

