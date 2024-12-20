using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Policlinic.Domain
{
    public class PoliclinicDbContextFactory : IDesignTimeDbContextFactory<PoliclinicDbContext>
    {
        public PoliclinicDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PoliclinicDbContext>();
            optionsBuilder.UseMySql(
                "Server=localhost;Database=policlinic;User=root;Password=vbhflf555;",
                ServerVersion.AutoDetect("Server=localhost;Database=policlinic;User=root;Password=vbhflf555;"));

            return new PoliclinicDbContext(optionsBuilder.Options);
        }
    }
}
