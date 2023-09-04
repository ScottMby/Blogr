using Blogr.Data;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            _context = context; //For database operations
            _userManager = userManager; //For getting the current user
        }

        /// <summary>
        /// Finds the current users blog posts.
        /// </summary>
        /// <returns>List of BlogDisplay</returns>
        [Route("api/GetBlogByCurrentUser")]
        [HttpGet]
        public async Task<ActionResult<List<BlogDisplay>>> GetUserBlogs()
        {
            try
            {
                List<BlogDisplay> blogList = new List<BlogDisplay>();
                ApplicationUser? user =
                    await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
                if (user != null)
                {
                    var userBlogs = _context.Blogs
                        .Where(u => u.User == user)
                        .Include("Content")
                        .ToList(); //To ensure that the blog content is also retrieve

                    user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
                    var result = await GetBlog.Get(userBlogs, user, _userManager);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Current User Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Returns the specified users blog posts.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of BlogDisplay</returns>
        [Route("api/GetBlogByUser")]
        [HttpGet]
        public async Task<ActionResult<List<BlogDisplay>>> GetUserBlogs(string userId)
        {
            try
            {
                List<BlogDisplay> blogList = new List<BlogDisplay>();
                ApplicationUser? user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    List<Blog> userBlogs = _context.Blogs
                        .Where(u => u.User == user)
                        .Include("Content")
                        .ToList(); //To ensure that the blog content is also retrieve

                    List<BlogDisplay> result = await GetBlog.Get(userBlogs, user, _userManager);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Current User Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}