using System;
using DropoutPrediction.Domain.Entities;

namespace DropoutPrediction.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository StudentRepository { get; }
    IStudentMetricsRepository StudentMetricsRepository { get; }
    IDropoutPredictionsRepository DropoutPredictionsRepository { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<bool> Complete();
}