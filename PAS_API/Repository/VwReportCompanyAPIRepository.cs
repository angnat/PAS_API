using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class VwReportCompanyAPIRepository : Repository<vw_Company_Header>, IVwReportCompanyAPIRepository
    {
        private readonly ApplicationDbContext _db;
        public VwReportCompanyAPIRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }      
        public Task<vw_Company_Header> UpdateAsync(vw_Company_Header entity)
        {
            throw new NotImplementedException();
        }
    }  
}
