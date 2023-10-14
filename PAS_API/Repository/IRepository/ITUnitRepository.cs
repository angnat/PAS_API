using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface ITUnitRepository : IRepository<TUnit>
    {
        Task<TUnit> UpdateAsync(TUnit entity);
    }
}
