using System;
using DropoutPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropoutPrediction.Infrastructure.Configs;

public class DropoutPredictionConfig : IEntityTypeConfiguration<DropoutPredictions>
{
    public void Configure(EntityTypeBuilder<DropoutPredictions> builder)
    {
        builder.ToTable("dropout_predictions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Probability)
            .HasPrecision(5, 4);

        builder.Property(x => x.RiskLevel)
            .HasConversion<string>();
    }
}