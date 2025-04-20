using System;
using Domain.ValueObjects;

namespace Domain.Entities;

public class FeedingSchedule
{
	public Guid Id_ { get; private set; }
	public Guid Animal_ { get; private set; }
	public EventTime FeedTime_ { get; private set; }
	public Food Food_ { get; private set; }
	public bool IsDone_ { get; private set; }

	public FeedingSchedule(Guid animal, EventTime feedTime, Food food)
	{
		Id_ = Guid.NewGuid();
		Animal_ = animal;
		FeedTime_ = feedTime;
		Food_ = food;
		IsDone_ = false;
	}

	void ChangeTime(EventTime newFeedTime)
	{
		FeedTime_ = newFeedTime;
	}

	public void Complete()
	{
		IsDone_ = true;
	}

	void Uncomplete()
	{
		IsDone_ = false;
	}
}

