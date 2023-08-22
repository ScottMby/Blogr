using Blogr.Data;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogr.Server.Controllers
{
    [Authorize]
    [ApiController]
    public class GetBlogByUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetBlogByUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("api/GetBlogByCurrentUser")]
        [HttpGet]
        public async Task<ActionResult<List<BlogDisplay>>> GetUserBlogs()
        {
            List<BlogDisplay> blogList = new List<BlogDisplay>();
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userBlogs = _context.Blogs.Where(u => u.b_User == user);
            foreach(var blog in userBlogs)
            {
                BlogDisplay blogDisplay = new BlogDisplay();
                blogDisplay.Title = blog.b_Title;
                blogDisplay.CreatorImgPath = user.u_Photo.path;
                blogDisplay.CreatorFirstName = user.u_FirstName;
                blogDisplay.CreatorLastName = user.u_LastName;
                blogDisplay.CreationDate = blog.b_CreationDate;
                blogDisplay.UpdatedDate = blog.b_CreationDate;
                blogDisplay.Content = blog.b_Content;
                blogList.Add(blogDisplay);
            }
            return Ok(blogList);
        }
        [Route("api/GetBlogByUser")]
        [HttpGet]
        public async Task<ActionResult<List<BlogDisplay>>> GetUserBlogs(string userId)
        {
            List<BlogDisplay> blogList = new List<BlogDisplay>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var blog in user.u_Blogs)
            {
                BlogDisplay blogDisplay = new BlogDisplay();
                blogDisplay.Title = blog.b_Title;
                blogDisplay.CreatorImgPath = user.u_Photo.path;
                blogDisplay.CreatorFirstName = user.u_FirstName;
                blogDisplay.CreatorLastName = user.u_LastName;
                blogDisplay.CreationDate = blog.b_CreationDate;
                blogDisplay.UpdatedDate = blog.b_CreationDate;
                blogDisplay.Content = blog.b_Content;
                blogList.Add(blogDisplay);
            }
            return Ok(blogList);
        }
    }
}
