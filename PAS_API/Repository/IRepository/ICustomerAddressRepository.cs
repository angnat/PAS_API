using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface ICustomerAddressRepository : IRepository<CustomerAddress>
    {
        Task<CustomerAddress> UpdateAsync(CustomerAddress entity);
    }
}
