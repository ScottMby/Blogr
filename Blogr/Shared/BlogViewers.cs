using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogr.Shared
{
    public class BlogViewers
    {
        [Key]
        public int Id { get; set; }
        public int AnalyticsId { get; set; }

        public BlogAnalytics Analytics = null!;
        public string UserId { get; set; }
    }
}
