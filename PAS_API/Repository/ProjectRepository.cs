using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        public ProjectRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Project> UpdateAsync(Project entity)
        {
            entity.ModifiedOn = DateTime.Now;
            _db.tblM_Project.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
