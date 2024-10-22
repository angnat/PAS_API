using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository.IRepository
{
    public interface IVwReportCompanyAPIRepository : IRepository<vw_Company_Header>
    {
        Task<vw_Company_Header> UpdateAsync(vw_Company_Header entity);
    }
}
