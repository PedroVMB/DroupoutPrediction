
using DropoutPrediction.Domain.Enums;


namespace DropoutPrediction.Domain.Entities;

public class DropoutPredictions : BaseEntity
{
    private RiskLevel riskLevel;
    private string? modelVersion;

    public DropoutPredictions(
        Guid studentId,
        decimal probability,
        RiskLevel riskLevel,
        string? modelVersion)
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        Probability = probability;
        RiskLevel = riskLevel;
        ModelVersion = modelVersion;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid StudentId { get; private set; }

    public decimal Probability { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
    public string? ModelVersion { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Student? Student { get; private set; }

   

}