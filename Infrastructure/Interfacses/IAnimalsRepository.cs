using System;
using Domain.Entities;

namespace Infrastructure.Interfacses;

public interface IAnimalsRepository
{
    Task<Animal?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Animal animal);
    Task<bool> RemoveAsync(Guid id);
    Task<IEnumerable<Animal>> GetAllAsync();
}

