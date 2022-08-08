using Microsoft.AspNetCore.Mvc;
using GManage.Models;

namespace GManage.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthDbContext _authDbContext;
        public AuthController(AuthDbContext context) => _authDbContext = context;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
