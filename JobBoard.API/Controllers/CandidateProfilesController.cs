using JobBoard.Core.DTOs;
using JobBoard.Core.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidateProfilesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CandidateProfilesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile(
        CandidateProfileDto dto)
    {
        var profile = new CandidateProfile
        {
            UserId = dto.UserId,
            Skills = dto.Skills,
            Experience = dto.Experience,
            Education = dto.Education,
            ResumeUrl = dto.ResumeUrl
        };

        _context.CandidateProfiles.Add(profile);

        await _context.SaveChangesAsync();

        return Ok(profile);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetProfile(int userId)
    {
        var profile = await _context.CandidateProfiles
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (profile == null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(
        int id,
        CandidateProfileDto dto)
    {
        var profile = await _context.CandidateProfiles
            .FindAsync(id);

        if (profile == null)
            return NotFound();

        profile.Skills = dto.Skills;
        profile.Experience = dto.Experience;
        profile.Education = dto.Education;
        profile.ResumeUrl = dto.ResumeUrl;

        await _context.SaveChangesAsync();

        return Ok(profile);
    }
}