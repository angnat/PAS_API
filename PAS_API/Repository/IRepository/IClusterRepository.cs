using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface IClusterRepository : IRepository<Cluster>
    {
        Task<Cluster> UpdateAsync(Cluster entity);    
    }
}
