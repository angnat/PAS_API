using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class VwAdminUnitReportAPIRepository : Repository<vw_AdminUnitReportAPI>, IVwAdminUnitReportAPIRepository
    {
        private readonly ApplicationDbContext _db;
        public VwAdminUnitReportAPIRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<vw_AdminUnitReportAPI> UpdateAsync(vw_AdminUnitReportAPI entity)
        {
            throw new NotImplementedException();
        }
    }
}
