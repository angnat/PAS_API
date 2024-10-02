using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository.IRepository
{
    public interface IVwAdminUnitReportAPIRepository : IRepository<vw_AdminUnitReportAPI>
    {
        Task<vw_AdminUnitReportAPI> UpdateAsync(vw_AdminUnitReportAPI entity);
    }
}
