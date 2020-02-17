using System.Threading.Tasks;

namespace ShopCart.Services
{
    public partial class CustomerService
    {
        public async Task<Models.Customer> GetById(int id)
        {
            return await _ctx.Customers.FindAsync(id);
        }
    }
}