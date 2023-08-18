using Blogr.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogr.Data
{
    public class Blog
    {
        [Key]
        private int b_ID { get; set; }

        private string b_Title { get; set; } = null!;

        [Key]
        private ApplicationUser b_user { get; set; } = null!;

        private DateTime b_CreationDate { get; set; }

        private DateTime b_UpdatedDate { get; set; }

        private string b_Content { get; set; } = null!;

    }
}
