using System;
using DropoutPrediction.Application.DTOs.Predictions;
using DropoutPrediction.Application.Interfaces;

namespace DropoutPrediction.Application.Services.Mock;

public class FakeMlService : IMlService
{
    public Task<PredictionResponseDto> PredictAsync(PredictionRequestDto request)
    {
        var probability = (decimal)new Random().NextDouble();
        return Task.FromResult(new PredictionResponseDto
        {
            Probability = probability,
            Risk = probability > 0.7m ? "HIGH" : "LOW"
        });
    }
}