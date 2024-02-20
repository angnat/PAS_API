using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class AdminUnitPengalihanRepository : Repository<AdminUnitPengalihan>, IAdminUnitPengalihanRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminUnitPengalihanRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<AdminUnitPengalihan> UpdateAsync(AdminUnitPengalihan entity)
        {
            throw new NotImplementedException();
        }
    }
}
