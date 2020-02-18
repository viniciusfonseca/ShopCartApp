using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopCart.Contexts;
using Newtonsoft.Json;

namespace ShopCart.Controllers
{
    public abstract class LoginBody
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public abstract class RegisterBody : LoginBody
    {
        [JsonProperty("password_confirm")]
        public string PasswordConfirm { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ShopCartContext _ctx;
        private readonly Services.UserService _userService;

        public UsersController(
            ILogger<CustomersController> logger,
            ShopCartContext ctx
        )
        {
            _logger = logger;
            _ctx = ctx;
            _userService = new Services.UserService(_ctx);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginBody body)
        {
            var result = await _userService.Login(body.Email, body.Password);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBody body)
        {
            var result = await _userService.Register(body.Email, body.Password);
            return Ok();
        }
    }
}