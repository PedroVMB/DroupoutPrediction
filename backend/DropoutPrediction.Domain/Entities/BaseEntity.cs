using System;

namespace DropoutPrediction.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; protected set; }
}
