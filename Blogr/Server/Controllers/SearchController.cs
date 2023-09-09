using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogr.Server.Controllers
{
    [Route("api/Search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SearchController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("{searchQuery}")]
        [HttpGet]
        public async Task<ActionResult<BlogDisplay>> Get([FromRoute(Name = "searchQuery")] string searchQuery)
        {
            try
            {
                List<Blog?> blogs = _context.Blogs
                    .Where(u => u.Title.Contains(searchQuery))
                    .Include("Content")
                    .Include("User")
                    .ToList();

                return Ok(GetBlog.Get(blogs, null, _userManager).Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
