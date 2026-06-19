using JobBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Job> Jobs => Set<Job>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Job>()
            .Property(j => j.Salary)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Job)
            .WithMany()
            .HasForeignKey(a => a.JobId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Candidate)
            .WithMany()
            .HasForeignKey(a => a.CandidateId);
    }
    public DbSet<CandidateProfile> CandidateProfiles
    => Set<CandidateProfile>();

    public DbSet<Application> Applications => Set<Application>();
}