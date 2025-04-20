using System;
using Domain.ValueObjects;
using MediatR;

namespace Domain.Events;

public class FeedingTimeEvent : INotification
{
	public Guid Animal_;
	public Food Food_;
	public Guid Enclosure_;

	public FeedingTimeEvent(Guid animal, Food food, Guid enclosure)
	{
		Animal_ = animal;
		Food_ = food;
		Enclosure_ = enclosure;
	}
}
