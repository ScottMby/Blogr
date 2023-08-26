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
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string CreatorImgPath { get; set; }

        public string CreatorFirstName { get; set; } = null!;

        public string CreatorLastName { get; set; } = null!;


        public DateTime CreationDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int ContentId { get; set; }

        public string ContentPath { get; set; }

        //public int Views { get; set; }

    }
}
