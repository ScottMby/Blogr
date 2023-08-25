using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogr.Server.Controllers
{
    [Route("api/DeleteBlog")]
    [ApiController]
    public class DeleteBlogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteBlogController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; //For database operations
            _userManager = userManager; //For getting the current user
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] int blogId)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user != null)
            {
                IsBlogOwnedByUser isBlogOwnedByUser = new IsBlogOwnedByUser(_context, _userManager);
                var userBlog = _context.Blogs
                .Where(u => u.b_ID == blogId)
                .Include("b_User")
                .FirstOrDefault();
                if(userBlog != null)
                {
                    if (userBlog.b_User == user)
                    {
                        _context.Blogs.Remove(userBlog);
                        _context.SaveChanges();
                        return Ok("Successfully Deleted");
                    }
                    else
                    {
                        return BadRequest("Current User Does Not Own Blog Post");
                    }
                }
                else
                {
                    return BadRequest("Blog Does Not Exist");
                }
            }
            else
            {
                return BadRequest("Failed to Get Current User");
            }
        }
    }
}


