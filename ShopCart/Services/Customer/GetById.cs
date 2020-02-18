using System.Threading.Tasks;
using ShopCart.Core;

namespace ShopCart.Services
{
    public partial class CustomerService
    {
        public async Task<Models.Customer> GetById(int id)
        {
            var customer = await _ctx.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new NotFoundError("Customer not found.");
            }
            return customer;
        }
    }
}