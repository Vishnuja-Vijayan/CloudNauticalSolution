using CloudNauticalSolution.Domain.AbstractClasses.IDomain;
using Microsoft.AspNetCore.Mvc;

namespace CloudNauticalSolution.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOdrderService;

        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOdrderService = customerOrderService;
        }

        [HttpPost("GetRecentOrder")]
        public async Task<IActionResult> GetRecentOrder([FromBody] dynamic request)
        {
            string email = request.user;
            string customerId = request.customerId;

            var response = await _customerOdrderService.GetRecentOrder(email, customerId);

            return Ok(response);
        }
    }
}
