using DropoutPrediction.Domain.Entities;
using DropoutPrediction.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DropoutPrediction.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger  = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        // Apply any pending migrations automatically
        await context.Database.MigrateAsync();

        if (await context.Students.AnyAsync())
        {
            logger.LogInformation("Database already seeded. Skipping.");
            return;
        }

        logger.LogInformation("Seeding database with sample students...");

        // ------------------------------------------------------------------
        // Seed data: 30 students with realistic metrics and varied risk levels
        // ------------------------------------------------------------------
        var seedData = new[]
        {
            // (name, age, course, semester, enrollmentOffset days, attendance, grades, assignments, lastAccess)
            ("Ana Clara Ferreira",     20, "Engenharia de Software",  3, -365, 0.92m, 8.5m, 9,  2),
            ("Bruno Henrique Lima",    22, "Ciência da Computação",   5, -540, 0.40m, 4.2m, 2, 45),
            ("Camila Souza",           19, "Sistemas de Informação",  2, -180, 0.78m, 7.1m, 7,  5),
            ("Diego Alves Martins",    24, "Engenharia de Software",  7, -720, 0.25m, 3.0m, 1, 60),
            ("Eduarda Costa",          21, "Ciência da Computação",   4, -420, 0.88m, 9.0m, 10, 1),
            ("Felipe Ramos",           23, "Análise e Desenvolvimento",6,-600, 0.55m, 5.8m, 5, 20),
            ("Gabriela Nunes",         20, "Sistemas de Informação",  3, -300, 0.95m, 9.3m, 10, 0),
            ("Henrique Batista",       25, "Engenharia de Software",  8, -840, 0.30m, 3.5m, 1, 55),
            ("Isabela Rodrigues",      19, "Ciência da Computação",   1,  -90, 0.80m, 7.8m, 8,  3),
            ("João Pedro Carvalho",    22, "Análise e Desenvolvimento",5,-500, 0.60m, 6.2m, 6, 15),
            ("Karoline Oliveira",      21, "Sistemas de Informação",  4, -400, 0.45m, 4.8m, 3, 38),
            ("Lucas Pereira",          20, "Engenharia de Software",  3, -310, 0.85m, 8.2m, 9,  4),
            ("Mariana Santos",         23, "Ciência da Computação",   6, -650, 0.72m, 7.5m, 7,  8),
            ("Nicolas Gomes",          19, "Análise e Desenvolvimento",2,-200, 0.20m, 2.8m, 0, 70),
            ("Olivia Moreira",         22, "Sistemas de Informação",  5, -480, 0.90m, 8.8m, 10, 1),
            ("Pedro Augusto Silva",    24, "Engenharia de Software",  7, -730, 0.35m, 4.0m, 2, 50),
            ("Quiteria Fernandes",     20, "Ciência da Computação",   3, -350, 0.75m, 7.0m, 7,  6),
            ("Rafael Mendes",          21, "Análise e Desenvolvimento",4,-440, 0.58m, 5.5m, 5, 22),
            ("Sara Lopes",             19, "Sistemas de Informação",  1, -100, 0.88m, 8.6m, 9,  2),
            ("Thiago Barbosa",         23, "Engenharia de Software",  6, -620, 0.42m, 4.4m, 3, 42),
            ("Ursula Dias",            22, "Ciência da Computação",   5, -510, 0.83m, 8.1m, 8,  4),
            ("Victor Hugo Teixeira",   24, "Análise e Desenvolvimento",7,-760, 0.22m, 2.5m, 0, 65),
            ("Wanessa Cardoso",        20, "Sistemas de Informação",  3, -320, 0.91m, 8.9m, 10, 1),
            ("Xande Correia",          21, "Engenharia de Software",  4, -410, 0.50m, 5.2m, 4, 28),
            ("Yasmin Azevedo",         19, "Ciência da Computação",   2, -190, 0.77m, 7.4m, 7,  6),
            ("Zeca Nascimento",        25, "Análise e Desenvolvimento",8,-800, 0.18m, 2.2m, 0, 80),
            ("Alice Monteiro",         20, "Engenharia de Software",  3, -340, 0.87m, 8.4m, 9,  3),
            ("Bernardo Cruz",          22, "Sistemas de Informação",  5, -520, 0.62m, 6.5m, 6, 12),
            ("Cecília Vieira",         21, "Ciência da Computação",   4, -430, 0.93m, 9.1m, 10, 1),
            ("Daniel Franco",          23, "Análise e Desenvolvimento",6,-680, 0.38m, 4.1m, 2, 48),
        };

        foreach (var (name, age, course, semester, offsetDays, attendance, grades, assignments, lastAccess) in seedData)
        {
            var enrollmentDate = DateTime.UtcNow.AddDays(offsetDays);

            var student = new Student(name, age, course, semester, enrollmentDate);
            context.Students.Add(student);
            await context.SaveChangesAsync(); // save to get the Id

            // Metrics
            var metrics = new StudentMetrics(
                student.Id,
                attendance,
                grades,
                assignments,
                lastAccess
            );
            context.StudentMetrics.Add(metrics);

            // Derive probability and risk from metrics
            var attendanceFactor  = 1m - attendance;
            var gradesFactor      = 1m - (grades / 10m);
            var accessFactor      = Math.Min(lastAccess / 60m, 1m);
            var assignmentFactor  = assignments > 0 ? 1m - Math.Min(assignments / 10m, 1m) : 1m;

            var probability = Math.Clamp(
                (attendanceFactor * 0.35m) + (gradesFactor * 0.35m) +
                (accessFactor * 0.20m) + (assignmentFactor * 0.10m),
                0m, 1m
            );

            var riskLevel = probability switch
            {
                > 0.65m => RiskLevel.High,
                > 0.35m => RiskLevel.Medium,
                _       => RiskLevel.Low
            };

            var prediction = new DropoutPredictions(
                student.Id,
                Math.Round(probability, 4),
                riskLevel,
                "v1-seed"
            );
            context.DropoutPredictions.Add(prediction);

            await context.SaveChangesAsync();
        }

        logger.LogInformation("Seeding complete. {Count} students inserted.", seedData.Length);
    }
}
