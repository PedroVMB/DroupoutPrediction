using System;
using DropoutPrediction.Application.DTOs.Predictions;
using DropoutPrediction.Application.Interfaces;

namespace DropoutPrediction.Application.Services.Mock;

public class FakeMlService : IMlService
{
    public Task<PredictionResponseDto> PredictAsync(PredictionRequestDto request)
    {
        // Derive a deterministic-ish probability from the real input values
        var attendanceFactor = 1m - request.Attendance;           // low attendance → higher risk
        var gradesFactor     = 1m - (request.Grades / 10m);       // low grades → higher risk
        var accessFactor     = Math.Min(request.LastAccess / 60m, 1m); // days since last access
        var assignmentFactor = request.Assignments > 0
            ? 1m - Math.Min(request.Assignments / 10m, 1m)
            : 1m;

        var rawScore = (attendanceFactor * 0.35m)
                     + (gradesFactor     * 0.35m)
                     + (accessFactor     * 0.20m)
                     + (assignmentFactor * 0.10m);

        // Add a small random jitter so repeated calls vary slightly
        var jitter = (decimal)(new Random().NextDouble() * 0.1 - 0.05);
        var probability = Math.Clamp(rawScore + jitter, 0m, 1m);

        var risk = probability switch
        {
            > 0.65m => "HIGH",
            > 0.35m => "MEDIUM",
            _       => "LOW"
        };

        return Task.FromResult(new PredictionResponseDto
        {
            Probability = Math.Round(probability, 4),
            Risk = risk
        });
    }
}