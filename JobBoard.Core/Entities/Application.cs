namespace JobBoard.Core.Entities;

public class Application
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public Job Job { get; set; } = null!;

    public int CandidateId { get; set; }

    public User Candidate { get; set; } = null!;

    public string Status { get; set; } = "Applied";

    public DateTime AppliedAt { get; set; }
}