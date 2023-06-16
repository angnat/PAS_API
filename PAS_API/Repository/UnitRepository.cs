using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _db;
        public UnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Unit> UpdateAsync(Unit entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblM_Unit.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }


}
