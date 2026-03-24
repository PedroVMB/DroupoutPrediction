using DropoutPrediction.Application.DTOs.Metrics;
using DropoutPrediction.Application.DTOs.Predictions;
using DropoutPrediction.Application.DTOs.Student;
using DropoutPrediction.Application.Interfaces;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Enums;
using DropoutPrediction.Domain.Interfaces;

namespace DropoutPrediction.Application.Services;

public class StudentService(
    IUnitOfWork unitOfWork,
    IMlService mlService
) : IStudentService
{
    public async Task<Guid> CreateStudentAsync(CreateStudentDto dto)
    {
        var student = new Student(
            dto.Name,
            dto.Age,
            dto.Course
        );

        unitOfWork.StudentRepository.Add(student);
        await unitOfWork.Complete();
        return student.Id;
    }

    public async Task<IEnumerable<StudentResponseDto>> GetAllAsync()
    {
        var students = await unitOfWork.StudentRepository.ListAllAsync();
        return students.Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Course = s.Course
        });
    }

    public async Task AddMetricsAsync(CreateStudentMetricsDto dto)
    {
        // 🔍 valida se aluno existe
        var exists = unitOfWork.StudentRepository.Exists(dto.StudentId);
        if (!exists)
            throw new Exception("Student not found");

        // 📊 cria métricas
        var metrics = new StudentMetrics(
            dto.StudentId,
            dto.AttendanceRate,
            dto.GradesAverage,
            dto.AssignmentsCompleted,
            dto.LastAccessDaysAgo
        );
        unitOfWork.StudentMetricsRepository.Add(metrics);

        // 🤖 chama ML
        var prediction = await mlService.PredictAsync(new PredictionRequestDto
        {
            Attendance = dto.AttendanceRate,
            Grades = dto.GradesAverage,
            LastAccess = dto.LastAccessDaysAgo,
            Assignments = dto.AssignmentsCompleted
        });

        // 🎯 converte risco
        var riskLevel = prediction.Risk switch
        {
            "HIGH" => RiskLevel.High,
            "MEDIUM" => RiskLevel.Medium,
            _ => RiskLevel.Low
        };

        // 💾 salva previsão
        var predictionEntity = new DropoutPredictions(
            dto.StudentId,
            prediction.Probability,
            riskLevel,
            "v1"
        );
        unitOfWork.DropoutPredictionsRepository.Add(predictionEntity);

        await unitOfWork.Complete();
    }
}