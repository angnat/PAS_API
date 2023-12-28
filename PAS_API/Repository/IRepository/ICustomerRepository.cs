using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> UpdateAsync(Customer entity);
    }
}
