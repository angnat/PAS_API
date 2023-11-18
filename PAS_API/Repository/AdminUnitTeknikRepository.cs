using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class AdminUnitTeknikRepository : Repository<AdminUnitTeknik>, IAdminUnitTeknikRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminUnitTeknikRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AdminUnitTeknik> UpdateAsync(AdminUnitTeknik entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblT_AdminUnitTeknik.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
