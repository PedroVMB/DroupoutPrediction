using System;

namespace DropoutPrediction.Application.DTOs.Metrics;

public class CreateStudentMetricsDto
{
    public Guid StudentId { get; set; }

    public decimal AttendanceRate { get; set; }
    public decimal GradesAverage { get; set; }
    public int AssignmentsCompleted { get; set; }
    public int LastAccessDaysAgo { get; set; }
}