using System;
using DropoutPrediction.Domain.Entities;

namespace DropoutPrediction.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    bool Exists(Guid id);
    Task<int> CountAsync();
}