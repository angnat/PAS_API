using Microsoft.EntityFrameworkCore;
using PAS_API.Model;

namespace PAS_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Unit> tblM_Unit { get; set; }
        public DbSet<TUnit> tblT_Unit { get; set; }
        public DbSet<AdminUnitTeknik> tblT_AdminUnitTeknik { get; set; }
    }
}
