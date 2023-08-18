using Blogr.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Blogr.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        //User Creation Date
        private DateTime u_CreationDate { get; set; }

        private string u_FirstName { get; set; } = null!;

        private string u_LastName { get; set; } = null!;
        //A string location of where a users photo is stored within the servers file system
        private string u_Photo { get; set; } = null!;

        private ICollection<Blog> u_Blogs { get; set; } = null!;
    }
}