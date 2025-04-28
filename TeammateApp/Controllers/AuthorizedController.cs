using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        [HttpPost]
        public async Task<IActionResult> Quit()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute("Main");
        }
        public IActionResult PostForm()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> PostForm(Post post)
        {
            string authorName = HttpContext.User.Identity.Name;
            post.Author = authorName;

            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            return RedirectToRoute("Profile");
        }
    }
}
