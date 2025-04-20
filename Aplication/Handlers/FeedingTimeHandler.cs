using System;
using MediatR;
using Domain.Events;

namespace Application.Handlers;

public class FeedingTimeHandler : INotificationHandler<FeedingTimeEvent>
{
    public Task Handle(FeedingTimeEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Животное {notification.Animal_} было покормлено в вольере {notification.Enclosure_} едой: {notification.Food_}");

        return Task.CompletedTask;
    }
}