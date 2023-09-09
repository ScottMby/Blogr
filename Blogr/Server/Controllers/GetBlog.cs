using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogr.Server.Controllers
{
    public static class GetBlog
    {
        /// <summary>
        /// Assigns all of the values within a BlogUpload list
        /// </summary>
        /// <param name="blogs">A list of Blogs to assign to a BlogUpload Object</param>
        /// <param name="user">Nullable: A user to get the details of</param>
        /// <param name="_userManager">The user manager for the controller</param>
        /// <returns>List<BlogUpload></returns>
        public static async Task<List<BlogDisplay>> Get(List<Blog> blogs, ApplicationUser? user, UserManager<ApplicationUser> _userManager)
        {
            List<BlogDisplay> blogDisplays = new List<BlogDisplay>();
            foreach (var blog in blogs)
            {
                if(user == null)
                {
                    user = await _userManager.FindByIdAsync(blog.User.Id);
                }
                BlogDisplay blogDisplay = new BlogDisplay
                {
                    Id = blog.ID,
                    Title = blog.Title,
                    Category = blog.Category,
                    CreatorImgPath = user.Photo.path ?? @"~\UserPhotos\default.png",
                    CreatorFirstName = user.FirstName,
                    CreatorLastName = user.LastName,
                    CreationDate = blog.CreationDate,
                    UpdatedDate = blog.UpdatedDate,
                    ContentId = blog.Content.Id,
                    ContentPath = blog.Content.path
                };
                blogDisplays.Add(blogDisplay);
            }

            return blogDisplays;
        }
    }
}
