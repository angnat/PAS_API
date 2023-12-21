using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class ClusterRepository : Repository<Cluster>, IClusterRepository
    {
        private readonly ApplicationDbContext _db;

        public ClusterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Cluster> UpdateAsync(Cluster entity)
        {
            entity.ModifiedOn = DateTime.Now;
            _db.tblM_Cluster.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
