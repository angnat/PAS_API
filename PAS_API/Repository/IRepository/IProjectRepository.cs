using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project> UpdateAsync(Project entity);
    }
}
