using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeammateApp.Models;
using TeammateApp.Models.ViewModels;

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
        public async Task<IActionResult> Profile()
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Name == name);

            List<Post> posts = await _db.Posts.Where(p => p.Author == name).ToListAsync();

            return View(new UserAndPosts(user, posts));
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
        public IActionResult MyPosts()
        {
            return PartialView();
        }

        public async Task<IActionResult> Publications()
        {
            List<Post> posts = await _db.Posts.ToListAsync();

            return View(posts);
        }
    }
}
