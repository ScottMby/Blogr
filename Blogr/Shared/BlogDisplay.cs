using Blogr.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogr.Shared
{
    public class BlogDisplay
    {        
        public string Title { get; set; } = null!;

        public string CreatorImgPath { get; set; }

        public string CreatorFirstName { get; set; } = null!;

        public string CreatorLastName { get; set; } = null!;


        public DateTime CreationDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public BlogContent Content { get; set; } = null!;

        public bool DisplayContent = false;
    }
}
