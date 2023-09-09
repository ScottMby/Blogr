using Blogr.Server.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogr.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        //User Creation Date
        public DateTime CreationDate { get; set; }
        [PersonalData]
        public string FirstName { get; set; } = null!;
        [PersonalData]
        public string LastName { get; set; } = null!;
        //A string location of where a users photo is stored within the servers file system
        [Key]
        public UserImage Photo { get; set; } = new UserImage();

        public ICollection<Blog> Blogs { get; set; } = null!;
    }
}