using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Themeproj.Data;
using Themeproj.Models;
using static System.Net.WebRequestMethods;

namespace Themeproj.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("CommentList", "Comment", new { id = comment.PostId });
        }

        [HttpGet]
        public async Task<IActionResult> CommentList(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            var comments = _context.Comments.Where(p => p.PostId == id).ToList();
            if (post == null)
            {
                return NotFound();
            }

            var Comment = new Comment()
            {
                PostId = post.Id
            };

            PostComments pc = new PostComments()
            {
                Comments = comments,
                Post = post,
                NewComment = Comment
            };

            return View(pc);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(PostComments comment)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // Check if the post exists
            var post = await _context.Posts.FindAsync(comment.NewComment.PostId);
            if (post == null)
            {
                return BadRequest("Invalid PostId");
            }
            comment.NewComment.Date = DateTime.Now;
            _context.Comments.Add(comment.NewComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("CommentList", new { id = comment.NewComment.PostId });
        }
    }
}
