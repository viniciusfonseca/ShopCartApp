using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopCart.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ShopCart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ShopCartContext _ctx;

        public CustomersController(ILogger<CustomersController> logger, ShopCartContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int count = 10, [FromQuery] int page = 1)
        {
            var result = await _ctx.Customers.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _ctx.Customers.FindAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Customer customer)
        {
            _ctx.Customers.Add(customer);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.Customer customer)
        {
            _ctx.Entry(customer).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _ctx.Customers.FindAsync(id);
            _ctx.Customers.Remove(result);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
