using System;

namespace DropoutPrediction.Domain.Entities;

public class Student : BaseEntity
{
    public Student(string name, int age, string course)
    {
        Name = name;
        Age = age;
        Course = course;
    }

    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }
    public string Course { get; private set; } = string.Empty;
    public DateTime EnrollmentDate { get; private set; }

    public ICollection<StudentMetrics> Metrics { get; private set; } = new List<StudentMetrics>();
    public ICollection<DropoutPredictions> Predictions { get; private set; } = new List<DropoutPredictions>();

    
}