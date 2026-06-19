using JobBoard.Core.DTOs;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DashboardController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = new DashboardStatsDto
        {
            TotalJobs =
                await _context.Jobs.CountAsync(),

            TotalApplications =
                await _context.Applications
                    .CountAsync(),

            Interviews =
                await _context.Applications
                    .CountAsync(x =>
                        x.Status == "Interview"),

            Selected =
                await _context.Applications
                    .CountAsync(x =>
                        x.Status == "Selected")
        };

        return Ok(stats);
    }
}