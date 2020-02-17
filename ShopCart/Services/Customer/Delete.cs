using System.Threading.Tasks;

namespace ShopCart.Services
{
    public partial class CustomerService
    {
        public async Task Delete(int id)
        {
            var result = await _ctx.Customers.FindAsync(id);
            _ctx.Customers.Remove(result);
            await _ctx.SaveChangesAsync();
        }
    }
}