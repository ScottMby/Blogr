using Blogr.Server.Models;
using Blogr.Shared;
using System.ComponentModel.DataAnnotations;

namespace Blogr.Data
{
    public class Blog
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public BlogContent Content { get; set; } = null!;

        public BlogAnalytics Analytics { get; set; } = null;

    }
}
