using System;
using DropoutPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropoutPrediction.Infrastructure.Configs;

public class StudentMetricsConfig : IEntityTypeConfiguration<StudentMetrics>
{
    public void Configure(EntityTypeBuilder<StudentMetrics> builder)
    {
        builder.ToTable("student_metrics");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AttendanceRate).HasPrecision(5, 2);
        builder.Property(x => x.GradesAverage).HasPrecision(5, 2);
    }
}