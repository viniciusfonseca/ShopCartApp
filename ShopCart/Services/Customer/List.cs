using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShopCart.Services
{
    public partial class CustomerService
    {
        public async Task<List<Models.Customer>> List()
        {
            return await _ctx.Customers.ToListAsync();
        }
    }
}