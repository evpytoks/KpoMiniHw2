using System;
namespace Domain.Events;
using MediatR;

public class AnimalMovedEvent : INotification
{
	public Guid Animal_;
	public Guid FromEnclosure_;
	public Guid ToEnclosure_;

	public AnimalMovedEvent(Guid animal, Guid fromEnclosure, Guid toEnclosure)
	{
		Animal_ = animal;
		FromEnclosure_ = fromEnclosure;
		ToEnclosure_ = toEnclosure;
	}
}
