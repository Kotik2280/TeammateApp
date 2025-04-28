using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeammateApp.Models;

namespace TeammateApp.Controllers
{
    [Authorize]
    public class AuthorizedController : Controller
    {
        DataContext _db;
        public AuthorizedController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Profile()
        {
            string name = HttpContext.User.Identity.Name;
            User user = _db.Users.FirstOrDefault(u => u.Name == name);

            return View(user);
        }
    }
}
