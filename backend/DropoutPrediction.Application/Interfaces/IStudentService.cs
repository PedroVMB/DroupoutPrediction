using System;
using DropoutPrediction.Application.DTOs.Metrics;
using DropoutPrediction.Application.DTOs.Student;

namespace DropoutPrediction.Application.Interfaces;

public interface IStudentService
{
    Task<Guid> CreateStudentAsync(CreateStudentDto dto);
    Task AddMetricsAsync(CreateStudentMetricsDto dto);
    Task<IEnumerable<StudentResponseDto>> GetAllAsync();
}