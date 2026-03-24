
using DropoutPrediction.Domain.Enums;

public DropoutPredictions(Guid studentId, decimal probability, RiskLevel riskLevel, string? modelVersion)
    {
        StudentId = studentId;
        Probability = probability;
        RiskLevel = riskLevel;
        ModelVersion = modelVersion;
        CreatedAt = DateTime.UtcNow;
    }
using System;
using DropoutPrediction.Domain.Enums;

namespace DropoutPrediction.Domain.Entities;

public class DropoutPredictions : BaseEntity
{
    private RiskLevel riskLevel;
    private string v;

    public DropoutPredictions(Guid studentId, decimal probability, RiskLevel riskLevel, string v)
    {
        StudentId = studentId;
        Probability = probability;
        this.riskLevel = riskLevel;
        this.v = v;
    }

    public Guid StudentId { get; private set; }

    public decimal Probability { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
    public string? ModelVersion { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Student? Student { get; private set; }

}