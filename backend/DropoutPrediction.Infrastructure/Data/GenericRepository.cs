using System;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DropoutPrediction.Infrastructure.Data;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public async Task<int> CountAsync()
    {
        var query = context.Set<T>().AsQueryable();

        
        return await query.CountAsync();
    }

    public bool Exists(Guid id)
    {
        return context.Set<T>().Any(x => x.Id == id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
}