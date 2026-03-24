using System;
using DropoutPrediction.Application.DTOs.Metrics;
using DropoutPrediction.Application.DTOs.Student;
using DropoutPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DropoutPrediction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // 📌 POST: api/students
    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto dto)
    {
        var studentId = await _studentService.CreateStudentAsync(dto);

        return CreatedAtAction(nameof(GetAll), new { id = studentId }, new
        {
            Id = studentId,
            Message = "Student created successfully"
        });
    }

    // 📌 GET: api/students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    // 📌 POST: api/students/metrics
    [HttpPost("metrics")]
    public async Task<IActionResult> AddMetrics([FromBody] CreateStudentMetricsDto dto)
    {
        try
        {
            await _studentService.AddMetricsAsync(dto);

            return Ok(new
            {
                Message = "Metrics and prediction saved successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message
            });
        }
    }
}