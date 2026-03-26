using System;

namespace DropoutPrediction.Domain.Entities;

public class Student : BaseEntity
{
    public Student(string name, int age, string course, int semester, DateTime enrollmentDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
        Course = course;
        Semester = semester;
        EnrollmentDate = enrollmentDate;
    }

    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }
    public string Course { get; private set; } = string.Empty;
    public int Semester { get; private set; }
    public DateTime EnrollmentDate { get; private set; }

    public ICollection<StudentMetrics> Metrics { get; private set; } = new List<StudentMetrics>();
    public ICollection<DropoutPredictions> Predictions { get; private set; } = new List<DropoutPredictions>();
}