namespace JobBoard.Core.Entities;

public class Job
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public string EmploymentType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}