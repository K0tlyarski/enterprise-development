using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Policlinic.Domain;

/// <summary>
/// Factory for creating instances of <see cref="PoliclinicDbContext"/> at design time.
/// </summary>
public class PoliclinicDbContextFactory : IDesignTimeDbContextFactory<PoliclinicDbContext>
{
    /// <summary>
    /// Creates a new instance of <see cref="PoliclinicDbContext"/> with the specified arguments.
    /// </summary>
    /// <param name="args">The arguments passed at design time.</param>
    /// <returns>A new instance of <see cref="PoliclinicDbContext"/>.</returns>
    public PoliclinicDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PoliclinicDbContext>();
        optionsBuilder.UseMySql(
            "Server=localhost;Database=policlinic;User=root;Password=vbhflf555;",
            ServerVersion.AutoDetect("Server=localhost;Database=policlinic;User=root;Password=vbhflf555;"));

        return new PoliclinicDbContext(optionsBuilder.Options);
    }
}
