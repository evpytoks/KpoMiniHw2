using System;
using Domain.Entities;

namespace Aplication.Interfaces;

public interface IAnimalTransferSetvice
{
    public Task AddAnimalAsync(Animal animal, Guid enclosure);
    public Task MoveAnimalAsync(Guid animal, Guid toEnclosure);
    public Task DeleteAnimalAsync(Guid animal);
    public Task AddEnclosureAsync(Enclosure enclosure);
    public Task DeleteEnclosureAsync(Guid enclosure);
}

