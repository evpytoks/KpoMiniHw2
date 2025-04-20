using System;
using Domain.Entities;

namespace Infrastructure.Interfacses;

public interface IEnclosuresRepository
{
    public Task<Enclosure?> GetByIdAsync(Guid id);
    public Task<bool> AddAsync(Enclosure enclosure);
    public Task<bool> RemoveAsync(Guid id);
    public Task<IEnumerable<Enclosure>> GetAllAsync();
}

