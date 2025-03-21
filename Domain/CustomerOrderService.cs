using CloudNauticalSolution.Domain.AbstractClasses.IDomain;
using CloudNauticalSolution.Domain.AbstractClasses.IRepository;
using CloudNauticalSolution.Models;

namespace CloudNauticalSolution.Domain
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderData _orderRepository;
        private const string NoCustomerExist = "No Customer Found";
        private const string NoEmailExist = "No Customer Found With Email Id";

        public CustomerOrderService(ICustomerOrderData orderRepository)
        {
            _orderRepository = orderRepository;
        }

       
        public async Task<OrderData> GetRecentOrder(string email, string customerId)
        {
            var result = (await _orderRepository.GetRecentOrder(customerId)).ToList();

            if (!result.Any())
                throw new Exception(NoCustomerExist);

            var orderResponse = result.First();

            if (!string.Equals(orderResponse.Email, email, StringComparison.OrdinalIgnoreCase))
                throw new Exception(NoEmailExist);

           
            var customerDetails = new Customer
            {
                FirstName = orderResponse.FirstName,
                LastName = orderResponse.LastName
            };

            var orderGroups = result.Where(r => r.OrderId != 0).GroupBy(r => r.OrderId);

            if (!orderGroups.Any())
            {
                return new OrderData { Customer = customerDetails, Order = new List<Order>() };
            }

            var orders = result.Where(r => r.OrderId != 0).GroupBy(r => r.OrderId).Select(orderGroup => new Order
                {
                    OrderNumber = orderGroup.Key,
                    OrderDate = orderResponse.OrderDate,
                    DeliveryExpected = orderResponse.DeliveryExpected,
                    DeliveryAddress = orderGroup.First().DeliveryAddress,
                    OrderItems = orderGroup.Select(r => new OrderItem
                    {
                        Product = r.ProductName ?? string.Empty,
                        Quantity = r.Quantity,
                        PriceEach = r.Price
                    }).ToList()
                }).ToList();

            return new OrderData { Customer = customerDetails, Order = orders };
        }

    }
}
