using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;
using System.Linq.Expressions;

namespace PAS_API.Repository
{
    public class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerAddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
        public async Task<CustomerAddress> UpdateAsync(CustomerAddress entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblT_CustomerAddress.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
