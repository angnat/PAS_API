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
        public DbSet<Progress> tblM_Progress { get; set; }
        public DbSet<Project> tblM_Project { get; set; }
        public DbSet<Cluster> tblM_Cluster { get; set; }
    }
}
