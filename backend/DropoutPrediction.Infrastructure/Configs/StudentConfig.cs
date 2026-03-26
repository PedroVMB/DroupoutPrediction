
using DropoutPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DropoutPrediction.Infrastructure.Configs;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Course)
            .HasMaxLength(100);

        builder.Property(x => x.Semester);
        builder.Property(x => x.EnrollmentDate);

        builder.HasMany(x => x.Metrics)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.StudentId);

        builder.HasMany(x => x.Predictions)
            .WithOne(x => x.Student)
            .HasForeignKey(x => x.StudentId);
    }
}