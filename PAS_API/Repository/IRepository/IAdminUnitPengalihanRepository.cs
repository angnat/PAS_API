using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IAdminUnitPengalihanRepository : IRepository<AdminUnitPengalihan>
    {
        Task<AdminUnitPengalihan> UpdateAsync(AdminUnitPengalihan entity);
    }
}
