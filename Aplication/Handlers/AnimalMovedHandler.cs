using System;
using Domain.Events;
using MediatR;

namespace Aplication.Handlers;

public class AnimalMovedHandler : INotificationHandler<AnimalMovedEvent>
{
    public Task Handle(AnimalMovedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Животное {notification.Animal_} переместили из вольера {notification.FromEnclosure_} в {notification.ToEnclosure_}.");

        return Task.CompletedTask;
    }
}
