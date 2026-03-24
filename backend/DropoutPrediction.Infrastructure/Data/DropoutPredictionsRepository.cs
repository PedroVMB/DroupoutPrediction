using System;
using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Interfaces;

namespace DropoutPrediction.Infrastructure.Data;

public class DropoutPredictionsRepository(AppDbContext context) : GenericRepository<DropoutPredictions>(context), IDropoutPredictionsRepository
{
}
