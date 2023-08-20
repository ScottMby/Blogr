using System.ComponentModel.DataAnnotations;

namespace Blogr.Server.Models
{
    public class BlogContent
    {
        [Key]
        public int Id { get; set; }

        public string path { get; set; } = null!;
    }
}
