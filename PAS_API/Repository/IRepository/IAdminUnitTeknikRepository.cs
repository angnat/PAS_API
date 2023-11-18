using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IAdminUnitTeknikRepository : IRepository<AdminUnitTeknik>
    {
        Task<AdminUnitTeknik> UpdateAsync(AdminUnitTeknik entity);
    }
}
