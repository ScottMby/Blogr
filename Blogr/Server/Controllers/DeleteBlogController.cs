using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blogr.Server.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Deletes a blog using its Id
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>ActionResult<string></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] int blogId)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            if (user != null)
            {
                IsBlogOwnedByUser isBlogOwnedByUser = new IsBlogOwnedByUser(_context, _userManager);
                var userBlog = _context.Blogs
                .Where(u => u.ID == blogId)
                .Include("User")
                .FirstOrDefault();
                if(userBlog != null)
                {
                    if (userBlog.User == user)
                    {
                        _context.Blogs.Remove(userBlog);
                        await _context.SaveChangesAsync();
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


