namespace ShopCart.Services
{
    public partial class CustomerService
    {
        private readonly Contexts.ShopCartContext _ctx;

        public CustomerService(Contexts.ShopCartContext ctx)
        {
            _ctx = ctx;
        }
    }
}