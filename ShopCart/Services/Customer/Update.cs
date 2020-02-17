using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShopCart.Services
{
    public partial class CustomerService
    {
        public async Task<Models.Customer> Update(int id, Models.Customer customer)
        {
            customer.Id = id;
            _ctx.Entry(customer).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return customer;
        }
    }
}