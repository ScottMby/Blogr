using Blogr.Client.Pages;
using Blogr.Data;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogr.Server.Controllers
{
    [Route("api/GetBlogByCategory")]
    [ApiController]
    public class GetBlogByCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetBlogByCategoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("{category}")]
        [HttpGet]
        public async Task<ActionResult<List<BlogDisplay>>> Get([FromRoute] string category)
        {
            List<BlogDisplay> blogList = new List<BlogDisplay>();

            var blogs = _context.Blogs
                .Where(b => b.Category == category)
                .Where(b => b.UpdatedDate > DateTime.Now.AddDays(-7))
                .Include("User")
                .Include("Analytics")
                .Include("Content")
                .OrderBy(b => b.Analytics.UniqueVisitors)
            .ToList();

            if (blogs != null)
            {
                foreach (Blog blog in blogs)
                {
                    BlogDisplay bg = new BlogDisplay();
                    bg.Id = blog.ID;
                    bg.Title = blog.Title;
                    bg.Category = blog.Category;

                    var user = await _userManager.FindByIdAsync(blog.User.Id);
                    if (user != null)
                    {
                        if (user.Photo.path != null)
                        {
                            bg.CreatorImgPath = user.Photo.path;
                        }
                        else
                        {
                            bg.CreatorImgPath = @"~\UserPhotos\default.png";
                        }
                        bg.CreatorFirstName = user.FirstName;
                        bg.CreatorLastName = user.LastName;
                        bg.CreationDate = blog.CreationDate;
                        bg.UpdatedDate = blog.UpdatedDate;
                        bg.ContentId = blog.Content.Id;
                        bg.ContentPath = blog.Content.path;
                        blogList.Add(bg);
                    }
                    else
                    {
                        return BadRequest("Blog Creator Not Found");
                    }
                }
            }
            else
            {
                return BadRequest("Blog Not Found");
            }
            return Ok(blogList);
        }
    }


}
