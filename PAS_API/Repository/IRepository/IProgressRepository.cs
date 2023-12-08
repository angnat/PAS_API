using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IProgressRepository : IRepository<Progress>
    {
        Task<Progress> UpdateAsync(Progress entity);
    }
}
