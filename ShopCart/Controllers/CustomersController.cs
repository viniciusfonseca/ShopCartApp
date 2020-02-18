using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopCart.Contexts;

namespace ShopCart.Controllers
{
    [ApiController]
    // [TypeFilter(typeof(ExceptionHandler))]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ShopCartContext _ctx;
        private readonly Services.CustomerService _customerService;

        public CustomersController(
            ILogger<CustomersController> logger,
            ShopCartContext ctx
        )
        {
            _logger = logger;
            _ctx = ctx;
            _customerService = new Services.CustomerService(_ctx);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int count = 10, [FromQuery] int page = 1)
        {
            var result = await _customerService.List();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _customerService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Customer customer)
        {
            await _customerService.Create(customer);
            return CreatedAtAction(nameof(GetOne), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.Customer customer)
        {
            var result = await _customerService.Update(id, customer);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.Delete(id);
            return Ok();
        }
    }
}
