using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class AdminUnitTeknikSilverRepository : Repository<AdminUnitTeknikSilver>, IAdminUnitTeknikSilverRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminUnitTeknikSilverRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AdminUnitTeknikSilver> UpdateAsync(AdminUnitTeknikSilver entity)
        {
            entity.ModifiedOn = DateTime.Now;
            _db.tblT_AdminUnitTeknik_Silver.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
