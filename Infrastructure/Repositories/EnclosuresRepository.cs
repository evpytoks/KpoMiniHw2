using System;
using Domain.Entities;
using Infrastructure.Interfacses;

namespace Infrastructure.Repositories;

public class EnclosuresRepository : IEnclosuresRepository
{
    private readonly List<Enclosure> Enclosures_ = new();

    public Task<Enclosure?> GetByIdAsync(Guid id)
    {
        var enclosure = Enclosures_.FirstOrDefault(e => e.Id_ == id);
        return Task.FromResult(enclosure);
    }

    public Task<bool> AddAsync(Enclosure enclosure)
    {
        if (Enclosures_.Any(e => e.Id_ == enclosure.Id_))
        {
            return Task.FromResult(false);
        }

        Enclosures_.Add(enclosure);
        return Task.FromResult(true);
    }

    public Task<bool> RemoveAsync(Guid id)
    {
        var enclosure = Enclosures_.FirstOrDefault(e => e.Id_ == id);
        if (enclosure is null)
        {
            return Task.FromResult(false);
        }

        Enclosures_.Remove(enclosure);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Enclosure>> GetAllAsync()
    {
        return Task.FromResult(Enclosures_.AsEnumerable());
    }
}
