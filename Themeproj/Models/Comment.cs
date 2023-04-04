namespace Themeproj.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }
        public string? Text { get; set; }
    }
}
