using ClipSnip.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClipSnip.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Explicitly configure Appointment -> ApplicationUser relationship to use UserId as FK
        modelBuilder.Entity<Appointment>()
            .HasOne(appointment => appointment.ApplicationUser)
            .WithMany(user => user.Appointments)
            .HasForeignKey(appointment => appointment.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}