namespace Themeproj.Models
{
    public class PostComments
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
    }
}
