using System;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DropoutPrediction.Infrastructure.Data;

public class StudentRepository(AppDbContext context)
    : GenericRepository<Student>(context), IStudentRepository
{
    public override async Task<IReadOnlyList<Student>> ListAllAsync()
    {
        return await context.Students
            .Include(s => s.Metrics)
            .Include(s => s.Predictions)
            .ToListAsync();
    }
}
