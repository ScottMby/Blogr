using Blogr.Client.Pages;
using Blogr.Data;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Blogr.Server.Controllers
{
    public class GetBlogBase : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetBlogBase(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; //For database operations
            _userManager = userManager; //For getting the current user
        }

        public async Task<ActionResult<List<BlogDisplay>>> GetBlogDisplays(List<Blog> blogs, bool currentUser)
        {
            try
            {
                List<BlogDisplay> blogDisplays = new List<BlogDisplay>();
                foreach (var blog in blogs)
                {
                    ApplicationUser? user;
                    if (currentUser)
                    {
                        user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
                    }
                    else
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

                return Ok(blogDisplays);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
