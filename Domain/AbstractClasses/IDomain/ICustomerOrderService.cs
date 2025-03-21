using CloudNauticalSolution.Models;

namespace CloudNauticalSolution.Domain.AbstractClasses.IDomain
{
    public interface ICustomerOrderService
    {
        Task<OrderData> GetRecentOrder(string email, string customerId);
    }
}
