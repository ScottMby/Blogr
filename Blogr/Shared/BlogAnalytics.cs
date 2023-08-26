using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogr.Shared
{
    public class BlogAnalytics
    {
        [Key]
        public int Id { get; set; }
        public int Views { get; set; }

        public int UniqueVisitors { get; set; }

        //Impletement Engamement and BOunce rate in the future

    }
}
