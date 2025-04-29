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
            post.Date = DateTime.Now;

            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            return RedirectToRoute("Profile");
        }
        public IActionResult MyPosts()
        {
            return PartialView();
        }
        public async Task<IActionResult> Publications(SortMethods sortMethod = SortMethods.DateAscending)
        {
            List<Post> posts = await _db.Posts.ToListAsync();

            List<Post> newPosts = new List<Post>();

            switch (sortMethod)
            {
                case SortMethods.DateAscending:
                    newPosts = posts.OrderBy(p => p.Date).ToList();
                    break;
                case SortMethods.DateDescending:
                    newPosts = posts.OrderByDescending(p => p.Date).ToList();
                    break;
                default:
                    break;
            }

            return View(new PostsAndSort(newPosts, sortMethod));
        }
        public async Task<IActionResult> Account(string name)
        {
            if (HttpContext.User.Identity.Name == name)
                return RedirectToRoute("Profile");

            User user = await _db.Users.FirstOrDefaultAsync(u => u.Name == name);
            if (user == null)
            {
                return NotFound();
            }

            List<Post> posts = await _db.Posts.Where(p => p.Author == name).ToListAsync();
            return View(new UserAndPosts(user, posts));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccout(int id)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            _db.Users.Remove(user);

            foreach (Post post in _db.Posts.Where(p => p.Author == user.Name))
            {
                post.Author = "Удалённый аккаунт";
            }

            await _db.SaveChangesAsync();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute("Main");
        }
    }
}
