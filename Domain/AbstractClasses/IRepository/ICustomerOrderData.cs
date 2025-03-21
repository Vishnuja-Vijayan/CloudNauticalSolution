using CloudNauticalSolution.Models;

namespace CloudNauticalSolution.Domain.AbstractClasses.IRepository
{
    public interface ICustomerOrderData
    {
        Task<IEnumerable<CustomerOrderDetails>> GetRecentOrder(string customerId);
    }
}
