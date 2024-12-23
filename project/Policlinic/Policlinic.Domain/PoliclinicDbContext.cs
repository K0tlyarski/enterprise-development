using Microsoft.EntityFrameworkCore;

namespace Policlinic.Domain;

/// <summary>
/// Represents the database context for the Policlinic domain.
/// </summary>
public class PoliclinicDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PoliclinicDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public PoliclinicDbContext(DbContextOptions<PoliclinicDbContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the Doctors DbSet.
    /// </summary>
    public DbSet<Doctor> Doctors { get; set; }

    /// <summary>
    /// Gets or sets the Patients DbSet.
    /// </summary>
    public DbSet<Patient> Patients { get; set; }

    /// <summary>
    /// Gets or sets the Receptions DbSet.
    /// </summary>
    public DbSet<Reception> Receptions { get; set; }

    /// <summary>
    /// Gets or sets the Specializations DbSet.
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }

    /// <summary>
    /// Configures the database schema for the context during model creation.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring the Reception entity
        modelBuilder.Entity<Reception>()
            .HasOne<Doctor>()
            .WithMany(d => d.Receptions)
            .HasForeignKey(r => r.DoctorId);

        modelBuilder.Entity<Reception>()
            .HasOne<Patient>()
            .WithMany(p => p.Receptions)
            .HasForeignKey(r => r.PatientId);

        // Configuring the Doctor entity
        modelBuilder.Entity<Doctor>()
            .HasOne<Specialization>()
            .WithMany(s => s.Doctors)
            .HasForeignKey(d => d.SpecializationId);

        base.OnModelCreating(modelBuilder);
    }
}
