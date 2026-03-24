using System;
using DropoutPrediction.Application.DTOs.Predictions;

namespace DropoutPrediction.Application.Interfaces;

public interface IMlService
{
    Task<PredictionResponseDto> PredictAsync(PredictionRequestDto request);
}