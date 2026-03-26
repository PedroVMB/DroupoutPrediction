using System;

namespace DropoutPrediction.Application.DTOs.Student;

public class StudentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public int Semester { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Latest metrics
    public decimal AttendanceRate { get; set; }
    public decimal GradeAverage { get; set; }
    public int AssignmentsCompleted { get; set; }
    public int LastAccessDaysAgo { get; set; }

    // Latest prediction
    public string RiskLevel { get; set; } = "LOW";
    public decimal Probability { get; set; }
}
