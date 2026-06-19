using JobBoard.Core.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();

        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job == null)
            return NotFound();

        return Ok(job);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(
    int id,
    JobDto dto)
    {
        var job =
            await _context.Jobs
            .FindAsync(id);

        if (job == null)
            return NotFound();

        job.Title = dto.Title;
        job.Description = dto.Description;
        job.Location = dto.Location;
        job.Salary = dto.Salary;
        job.EmploymentType =
            dto.EmploymentType;

        await _context.SaveChangesAsync();

        return Ok(job);
    }

    //[Authorize(Roles = "Recruiter")]
    [HttpPost]
    public async Task<IActionResult> CreateJob(JobDto dto)
    {
        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            Salary = dto.Salary,
            EmploymentType = dto.EmploymentType,
            CreatedAt = DateTime.UtcNow
        };

        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return Ok(job);
    }

}