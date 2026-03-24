using System;

namespace DropoutPrediction.Application.DTOs.Predictions;

public class PredictionRequestDto
{
    public decimal Attendance { get; set; }
    public decimal Grades { get; set; }
    public int LastAccess { get; set; }
    public int Assignments { get; set; }
}

public class PredictionResponseDto
{
    public decimal Probability { get; set; }
    public string Risk { get; set; } = string.Empty;
}