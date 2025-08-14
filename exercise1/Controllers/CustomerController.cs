using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exercise1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly NotificationSettingsConfig _options;
        private readonly ICustomerRepository _icustomerRepository;

        public CustomerController(IOptionsSnapshot<NotificationSettingsConfig> options, ICustomerRepository icustomerRepository)
        {
            _options = options.Value;
            _icustomerRepository = icustomerRepository;
        }



        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _icustomerRepository.getAllCustomers();
            return Ok(customers);
        }


        // POST api/<CustomerController>
        [HttpPost]
        public async Task<Results<Created<CustomerModel>,ValidationProblem>> AddCustomer([FromBody] CustomerModel customer)
        {
            var SavedCustomer = await _icustomerRepository.addCustomer(customer);
           
            return TypedResults.Created($"api/products/{SavedCustomer.GuId}",SavedCustomer);
        }

       
    }
}
