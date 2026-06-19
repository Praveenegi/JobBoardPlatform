namespace JobBoard.Core.DTOs;

public class DashboardStatsDto
{
    public int TotalJobs { get; set; }

    public int TotalApplications { get; set; }

    public int Interviews { get; set; }

    public int Selected { get; set; }
}