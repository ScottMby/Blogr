using Blogr.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogr.Data
{
    public class Blog
    {
        [Key]
        public int b_ID { get; set; }

        public string b_Title { get; set; } = null!;

        public ApplicationUser b_User { get; set; } = null!;

        public DateTime b_CreationDate { get; set; }

        public DateTime b_UpdatedDate { get; set; }

        public string b_Content { get; set; } = null!;

        public ICollection<Image> b_Images { get; set; } = null!;

    }
}
