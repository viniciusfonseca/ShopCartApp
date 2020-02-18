using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopCart.Filters
{
    public class AuthFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            
        }
    }
}