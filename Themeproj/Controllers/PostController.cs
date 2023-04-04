using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Logging;
using Themeproj.Data;
using Themeproj.Models;

namespace Themeproj.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePost(int id)
        {
            
            var theme = _context.Themes.Find(id);
            if (theme == null)
            {
              
                return NotFound();
            }
            var post = new Post
            {
                ThemeId = id,
                
            };

            return View(post);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost(Post newPost)
        {
            if(ModelState.IsValid!=true)
            {
                _context.Posts.Add(newPost);
                _context.SaveChanges();
                return RedirectToAction("ThemeList", "Theme",newPost.ThemeId);
            }
            return RedirectToAction("CreatePost", newPost.ThemeId);
           
        }



    }
}
