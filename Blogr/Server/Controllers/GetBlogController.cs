using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogr.Server.Controllers
{
    [Route("api/GetBlogById")]
    [ApiController]
    public class GetBlogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetBlogController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("{blogID}")]
        [HttpGet]
        public async Task<ActionResult<BlogDisplay>>Get([FromRoute(Name = "blogID")] int blogId)
        {
            BlogDisplay bg = new BlogDisplay();
            var blog = _context.Blogs
                .Where(u => u.ID == blogId)
                .Include("Content")
                .Include("User")
                .FirstOrDefault();
            if (blog != null)
            {
                bg.Id = blog.ID;
                bg.Title = blog.Title;

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
                }
                else
                {
                    return BadRequest("Blog Creator Not Found");
                }
            }
            else
            {
                return BadRequest("Blog Not Found");
            }
            

            return Ok(bg);
        }
    }
}
