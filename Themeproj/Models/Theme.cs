using Microsoft.Extensions.Hosting;

namespace Themeproj.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
