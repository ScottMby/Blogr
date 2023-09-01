using Blogr.Data;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogr.Server.Controllers
{
    [Route("api/GetBlogById")]
    [ApiController]
    public class GetBlogController : GetBlogBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetBlogController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base (context, userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("{blogID}")]
        [HttpGet]
        public async Task<ActionResult<BlogDisplay>>Get([FromRoute(Name = "blogID")] int blogId)
        {
            try
            {
                Blog? blog = _context.Blogs
                    .Where(u => u.ID == blogId)
                    .Include("Content")
                    .Include("User")
                    .FirstOrDefault();

                List<Blog?> blogsList = new List<Blog?> { blog };

                return Ok(GetBlogDisplays(blogsList, false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
