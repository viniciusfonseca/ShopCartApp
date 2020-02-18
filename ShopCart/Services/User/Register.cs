using System.Threading.Tasks;

namespace ShopCart.Services
{
    public partial class UserService
    {
        public async Task<Models.User> Register(string email, string password)
        {
            var user = _ctx.Users.Add(new Models.User {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            }).Entity;
            await _ctx.SaveChangesAsync();
            return user;
        }
    }
}