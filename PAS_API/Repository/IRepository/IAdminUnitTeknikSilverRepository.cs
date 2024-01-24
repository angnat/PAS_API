using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IAdminUnitTeknikSilverRepository : IRepository<AdminUnitTeknikSilver>
    {
        Task<AdminUnitTeknikSilver> UpdateAsync(AdminUnitTeknikSilver entity);
    }
}
