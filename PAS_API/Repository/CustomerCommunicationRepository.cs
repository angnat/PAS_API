using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;
using System.Linq.Expressions;

namespace PAS_API.Repository
{
    public class CustomerCommunicationRepository : Repository<CustomerCommunication>, ICustomerCommunicationRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerCommunicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
     
        public async Task<CustomerCommunication> UpdateAsync(CustomerCommunication entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblT_CustomerCommunication.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
