using Microsoft.EntityFrameworkCore;

namespace Policlinic.Domain
{
    public class PoliclinicDbContext : DbContext
    {
        public PoliclinicDbContext(DbContextOptions<PoliclinicDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Reception> Receptions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

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
}
