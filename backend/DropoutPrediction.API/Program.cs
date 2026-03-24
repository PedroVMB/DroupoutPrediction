using DropoutPrediction.Domain.Interfaces;
using DropoutPrediction.Application.Interfaces;
using DropoutPrediction.Application.Services;
using DropoutPrediction.Application.Services.Mock;
using DropoutPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 🔹 Add Controllers
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// 🔹 Repositórios e UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentMetricsRepository, StudentMetricsRepository>();
builder.Services.AddScoped<IDropoutPredictionsRepository, DropoutPredictionsRepository>();

// 🔹 Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IMlService, FakeMlService>();


// 🔹 Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 Map Controllers
app.MapControllers();

app.Run();