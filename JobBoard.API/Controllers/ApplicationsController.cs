using JobBoard.API.Services;
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

    private readonly EmailService _emailService;

    public ApplicationsController(
        ApplicationDbContext context,
        EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
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

                        ResumeUrl =
                _context.CandidateProfiles
                    .Where(cp =>
                        cp.UserId ==
                        a.CandidateId)
                    .Select(cp =>
                        cp.ResumeUrl)
                    .FirstOrDefault(),

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

        var candidate = await _context.Users
            .FindAsync(application.CandidateId);

        var job = await _context.Jobs
            .FindAsync(application.JobId);

        if (candidate != null && job != null)
        {
            string emailBody = application.Status switch
            {
            "Interview" => $@"
            Dear {candidate.FirstName},

            Congratulations!

            Your application for the position of
            '{job.Title}' has been shortlisted for an interview.

            Our recruitment team will contact you with
            further details shortly.

            Best Regards,
            Recruitment Team
            JobBoard Platform",

            "Selected" => $@"
            Dear {candidate.FirstName},

            Congratulations!

            We are pleased to inform you that you
            have been selected for the position of
            '{job.Title}'.

            Our team will contact you regarding the
            next steps.

            Best Regards,
            Recruitment Team
            JobBoard Platform",

            "Rejected" => $@"
            Dear {candidate.FirstName},

            Thank you for your interest in the position
            '{job.Title}'.

            After careful consideration, we regret to
            inform you that we will not be moving
            forward with your application at this time.

            We appreciate your interest and wish you
            success in your future endeavors.

            Best Regards,
            Recruitment Team
            JobBoard Platform",

            _ => $@"
            Dear {candidate.FirstName},

            Your application for the position
            '{job.Title}' has been updated.

            Current Status: {application.Status}

            Best Regards,
            Recruitment Team
            JobBoard Platform"
            };

            try
            {
                await _emailService.SendEmailAsync(
                    candidate.Email,
                    $"Application Update - {job.Title}",
                    emailBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Email Error: {ex.Message}");
            }
        }

        return Ok(application);
    }
}