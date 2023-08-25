using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogr.Shared
{
    public class BlogUpload
    {
        public string Title { get; set; }
        public string ContentPath { get; set; }

        public int? BlogId { get; set; }
    }
}
