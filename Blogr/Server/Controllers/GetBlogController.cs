using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


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
            try
            {
                Blog? blog = _context.Blogs
                    .Where(u => u.ID == blogId)
                    .Include("Content")
                    .Include("User")
                    .FirstOrDefault();

                List<Blog?> blogsList = new List<Blog?> { blog };

                return Ok(GetBlog.Get(blogsList, null, _userManager).Result[0]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
