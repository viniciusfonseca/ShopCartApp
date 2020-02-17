using Microsoft.Extensions.DependencyInjection;


namespace ShopCart.Tests.Utils
{
    public static class Globals
    {
        public static ServiceProvider ServiceProvider { get; set; }

        public static Contexts.ShopCartContext GetContext()
        {
            return (Contexts.ShopCartContext) ServiceProvider.GetService(typeof(Contexts.ShopCartContext));
        }
    }
}