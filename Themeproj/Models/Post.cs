namespace Themeproj.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Theme Theme { get; set; }
        public int ThemeId { get; set; }
        public string? Annotation { get; set; }
        public string? Text { get; set; }
        public string? Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
