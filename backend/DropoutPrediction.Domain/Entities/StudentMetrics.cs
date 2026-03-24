
    public StudentMetrics(Guid studentId, decimal attendanceRate, decimal gradesAverage, int assignmentsCompleted, int lastAccessDaysAgo)
    {
        StudentId = studentId;
        AttendanceRate = attendanceRate;
        GradesAverage = gradesAverage;
        AssignmentsCompleted = assignmentsCompleted;
        LastAccessDaysAgo = lastAccessDaysAgo;
        CreatedAt = DateTime.UtcNow;
    }
using System;

namespace DropoutPrediction.Domain.Entities;

public class StudentMetrics : BaseEntity
{
    public StudentMetrics(Guid studentId, decimal attendanceRate, decimal gradesAverage, int assignmentsCompleted, int lastAccessDaysAgo)
    {
        StudentId = studentId;
        AttendanceRate = attendanceRate;
        GradesAverage = gradesAverage;
        AssignmentsCompleted = assignmentsCompleted;
        LastAccessDaysAgo = lastAccessDaysAgo;
    }

    public Guid StudentId { get; private set; }

    public decimal AttendanceRate { get; private set; }
    public decimal GradesAverage { get; private set; }
    public int AssignmentsCompleted { get; private set; }
    public int LastAccessDaysAgo { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Student? Student { get; private set; }


}