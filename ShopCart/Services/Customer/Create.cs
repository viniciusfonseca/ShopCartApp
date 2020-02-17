using System.Threading.Tasks;

namespace ShopCart.Services
{
    public partial class CustomerService
    {
        public async Task<Models.Customer> Create(Models.Customer customer)
        {
            _ctx.Customers.Add(customer);
            await _ctx.SaveChangesAsync();
            return customer;
        }
    }
}