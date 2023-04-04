using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Themeproj.Data;
using Themeproj.Models;

namespace Themeproj.Controllers
{
    public class ThemeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThemeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ThemeList()
        {
            return View(await _context.Themes.ToListAsync());
        }

        
        public async Task<IActionResult> Choose(int? id)
        {
            var themes = await _context.Themes.FindAsync(id);
            List<Post> posts = _context.Posts.Where(p => p.ThemeId == id).ToList();
            ThemePosts tp = new ThemePosts
            {
                Posts = posts,
                Theme = themes
            };

            if (themes == null)
            {
                return NotFound();
            }

            return View(tp);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }

            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ThemeList));
        }



        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Theme newTheme)
        {
            var theme = newTheme;
            _context.Themes.Add(theme);
            await _context.SaveChangesAsync();
            return RedirectToAction("ThemeList");
        }

    }
}
