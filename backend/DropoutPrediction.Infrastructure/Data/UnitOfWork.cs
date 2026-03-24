using System;
using System.Collections.Concurrent;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DropoutPrediction.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    private readonly AppDbContext _context = context;

    private IStudentRepository? _studentRepository;
    public IStudentRepository StudentRepository =>
        _studentRepository ??= new StudentRepository(_context);

    private IStudentMetricsRepository? _studentMetricsRepository;
    public IStudentMetricsRepository StudentMetricsRepository =>
        _studentMetricsRepository ??= new StudentMetricsRepository(_context);

    private IDropoutPredictionsRepository? _dropoutPredictionsRepository;
    public IDropoutPredictionsRepository DropoutPredictionsRepository =>
        _dropoutPredictionsRepository ??= new DropoutPredictionsRepository(_context);

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, t =>
        {
            var repositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(repositoryType, _context) ?? throw new InvalidOperationException($"Could not create repository instance for {t}");
        });
    }

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
