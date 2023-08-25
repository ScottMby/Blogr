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
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<bool>> Get([FromQuery] int blogId) 
        {
            var blog = _context.Blogs.Where(u => u.b_ID == blogId)
                .Include("b_User")
                .FirstOrDefault();

            var user = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));


            if (blog.b_User == user.Result)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }


            
        }
    }
}
