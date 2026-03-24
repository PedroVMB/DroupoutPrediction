using System;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Interfaces;

namespace DropoutPrediction.Infrastructure.Data;

public class StudentMetricsRepository(AppDbContext context) : GenericRepository<StudentMetrics>(context), IStudentMetricsRepository
{
}
