using System;
using Domain.Entities;
using Infrastructure.Interfacses;

namespace Infrastructure.Repositories;

public class AnimalRepository : IAnimalsRepository
{
    private readonly List<Animal> Animals_ = new();

    public Task<Animal?> GetByIdAsync(Guid id)
    {
        var animal = Animals_.FirstOrDefault(a => a.Id_ == id);
        return Task.FromResult(animal);
    }

    public Task<bool> AddAsync(Animal animal)
    {
        if (Animals_.Any(a => a.Id_ == animal.Id_))
        {
            return Task.FromResult(false);
        }

        Animals_.Add(animal);
        return Task.FromResult(true);
    }

    public Task<bool> RemoveAsync(Guid id)
    {
        var animal = Animals_.FirstOrDefault(a => a.Id_ == id);
        if (animal is null)
        {
            return Task.FromResult(false);
        }

        Animals_.Remove(animal);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Animal>> GetAllAsync()
    {
        return Task.FromResult(Animals_.AsEnumerable());
    }
}
