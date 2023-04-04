using Microsoft.AspNetCore.Identity;
using Themeproj.Models;

namespace Themeproj.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any themes.
            if (context.Themes.Any())
            {
                return;   // DB has been seeded
            }

            var themes = new Theme[]
            {
                new Theme{Name= "Nature", Description="Discuss anything related to nature"},
                new Theme{Name= "Technology", Description="Discuss anything related to technology"}
            };
            foreach (Theme t in themes)
            {
                context.Themes.Add(t);
            }
            context.SaveChanges();



            if (context.Posts.Any()) return;
            var posts = new Post[]
            {
                new Post{PublicationDate = DateTime.Now,Annotation= "Amazing sunset", Text= "I just saw an amazing sunset today and wanted to share with everyone", ThemeId= themes[0].Id, Theme = themes[0],Author = "Ivan"},
                new Post{PublicationDate = DateTime.Now, Annotation= "New smartphone release", Text= "The new smartphone was just released, what are your thoughts on it", ThemeId=themes[1].Id, Theme = themes[1], Author = "Ruden"}
            };
            foreach (Post p in posts)
            {
                context.Posts.Add(p);
            }
            context.SaveChanges();

            if (context.Comments.Any()) return;
            var comments = new Comment[]
            {
                new Comment{Text="Wow, what a beautiful sunset!", PostId= posts[0].Id, Author = "meuf"},
                new Comment{Text="I can't wait to get my hands on it!", PostId=posts[1].Id, Author = "meuf"}
            };
            foreach (Comment c in comments)
            {
                context.Comments.Add(c);
            }
            context.SaveChanges();
        }
    }

}
