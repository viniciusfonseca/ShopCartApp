using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopCart.Services
{
    public partial class UserService
    {
        public async Task<Models.User> Login(string email, string password)
        {
            var user = await _ctx.Users.FirstAsync(x => x.Email == email);
            if (user == null)
                throw new Core.NotAuthorizedError();
            var verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!verified)
                throw new Core.NotAuthorizedError();
            return user;
        }
    }
}