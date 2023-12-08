using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class ProgressRepository : Repository<Progress>, IProgressRepository
    {
        private readonly ApplicationDbContext _db;

        public ProgressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Progress> UpdateAsync(Progress entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblM_Progress.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
