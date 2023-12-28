using PAS_API.Model;

namespace PAS_API.Repository.IRepository
{
    public interface ICustomerCommunicationRepository : IRepository<CustomerCommunication>
    {
        Task<CustomerCommunication> UpdateAsync(CustomerCommunication entity);
    }
}
