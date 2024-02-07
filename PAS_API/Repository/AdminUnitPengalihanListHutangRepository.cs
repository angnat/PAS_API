using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class AdminUnitPengalihanListHutangRepository : Repository<AdminUnitPengalihanListHutang>, IAdminUnitPengalihanListHutangRepository
    {
        private readonly ApplicationDbContext _db;

        public AdminUnitPengalihanListHutangRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AdminUnitPengalihanListHutang> UpdateAsync(AdminUnitPengalihanListHutang entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblT_AdminUnitPengalihanListHutang.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
