using PAS_API.Data;
using PAS_API.Model;
using PAS_API.Repository.IRepository;

namespace PAS_API.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Customer> UpdateAsync(Customer entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _db.tblM_Customer.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }    
}
