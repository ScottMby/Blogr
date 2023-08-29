using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blogr.Server.Controllers
{
    [Route("api/IsBlogOwnedByUser")]
    [ApiController]
    public class IsBlogOwnedByUser : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IsBlogOwnedByUser(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; //For database operations
            _userManager = userManager; //For getting the current user
        }
        /// <summary>
        /// Returns if the specified blog is owned by the user
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>true or false</returns>
        [HttpGet]
        public async Task<ActionResult<bool>> GetIsBlogOwnedByUser([FromQuery] int blogId) 
        {
            var blog = _context.Blogs.Where(u => u.ID == blogId)
                .Include("User")
                .FirstOrDefault();
            if(blog != null)
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (claim != null)
                {
                    var user = await _userManager.FindByIdAsync(claim);


                    if (user != null)
                    {
                        if (blog.User == user)
                        {
                            return Ok(true);
                        }
                        else
                        {
                            return Ok(false);
                        }
                    }
                    else
                    {
                        return BadRequest("Current User Not Found");
                    }
                }
                else
                {
                    return BadRequest("User Claim is Null");
                }
            }
            else
            {
                return BadRequest("Blog Doesn't Exist");
            }
            


            
        }
    }
}
