namespace ShopCart.Services
{
    public partial class UserService
    {
        private readonly Contexts.ShopCartContext _ctx;

        public UserService(Contexts.ShopCartContext ctx)
        {
            _ctx = ctx;
        }
    }
}