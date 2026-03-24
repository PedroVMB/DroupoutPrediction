using System;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Interfaces;

namespace DropoutPrediction.Infrastructure.Data;

public class StudentRepository(AppDbContext context) : GenericRepository<Student>(context), IStudentRepository
{
}
