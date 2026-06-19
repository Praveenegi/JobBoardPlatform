using JobBoard.Core.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ApplicationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Apply(ApplicationDto dto)
    {
        var existingApplication =
        await _context.Applications
        .FirstOrDefaultAsync(x =>
            x.JobId == dto.JobId &&
            x.CandidateId == dto.CandidateId);

        if (existingApplication != null)
        {
            return BadRequest(
                "Already Applied");
        }

        var application = new Application
        {
            JobId = dto.JobId,
            CandidateId = dto.CandidateId,
            Status = "Applied",
            AppliedAt = DateTime.UtcNow
        };

        _context.Applications.Add(application);

        await _context.SaveChangesAsync();

        return Ok(application);
    }

    [HttpGet("job/{jobId}")]
    public async Task<IActionResult> GetApplicationsForJob(int jobId)
    {
        var applications = await _context.Applications
            .Include(a => a.Candidate)
            .Where(a => a.JobId == jobId)
            .Select(a => new
            {
                ApplicationId = a.Id,
                CandidateId = a.CandidateId,
                CandidateName =
                    a.Candidate.FirstName + " " +
                    a.Candidate.LastName,
                CandidateEmail =
                    a.Candidate.Email,
                Status = a.Status,
                AppliedAt = a.AppliedAt
            })
            .ToListAsync();

        return Ok(applications);
    }

    [HttpGet("candidate/{candidateId}")]
    public async Task<IActionResult> GetApplicationsForCandidate(int candidateId)
    {
        var applications = await _context.Applications
            .Where(a => a.CandidateId == candidateId)
            .ToListAsync();

        return Ok(applications);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        ApplicationStatusDto dto)
    {
        var application = await _context.Applications
            .FindAsync(id);

        if (application == null)
            return NotFound();

        application.Status = dto.Status;

        await _context.SaveChangesAsync();

        return Ok(application);
    }
}