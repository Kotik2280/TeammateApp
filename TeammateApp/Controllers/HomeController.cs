using TeammateApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TeammateApp.Attributes;

namespace TeammateApp.Controllers
{
    [AutorizeRedirect]
    public class HomeController : Controller
    {
        public DataContext _db;
        public HomeController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return RedirectToRoute("Main");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            User? loginUser = _db.Users.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);

            if (loginUser is null)
                return RedirectToRoute("Main");

            List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Name, user.Name) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return RedirectToRoute("Profile");
        }
    }
}
