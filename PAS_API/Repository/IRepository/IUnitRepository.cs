using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit> UpdateAsync(Unit entity);
    }
}
