using System;

namespace DropoutPrediction.Application.DTOs.Student;

public class StudentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
}
