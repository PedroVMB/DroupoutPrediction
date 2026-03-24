using System;
using DropoutPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DropoutPrediction.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentMetrics> StudentMetrics { get; set; }
    public DbSet<DropoutPredictions> DropoutPredictions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}