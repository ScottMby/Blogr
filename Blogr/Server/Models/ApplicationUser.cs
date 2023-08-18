using Blogr.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blogr.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        //User Creation Date
        public DateTime u_CreationDate { get; set; }

        public string u_FirstName { get; set; } = null!;

        public string u_LastName { get; set; } = null!;
        //A string location of where a users photo is stored within the servers file system
        public string u_Photo { get; set; } = null!;

        public ICollection<Blog> u_Blogs { get; set; } = null!;
    }
}