using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class TUnitRepository : Repository<TUnit>, ITUnitRepository
    {
        private readonly ApplicationDbContext _db;
        public TUnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       
        public async Task<TUnit> UpdateAsync(TUnit entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblT_Unit.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }   
}
