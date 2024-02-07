using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IAdminUnitPengalihanListHutangRepository : IRepository<AdminUnitPengalihanListHutang>
    {
        Task<AdminUnitPengalihanListHutang> UpdateAsync(AdminUnitPengalihanListHutang entity);
    }
}
