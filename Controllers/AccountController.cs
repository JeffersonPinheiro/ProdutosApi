using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Repositories;

namespace ProdutosApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;
        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            return View();
        }
    }
}
