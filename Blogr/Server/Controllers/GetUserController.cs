using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogr.Server.Controllers
{
    [Authorize]
    [Route("/api/GetUserId")]
    [ApiController]
    public class GetUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (user != null)
                {
                    var userId = user.Id;
                    return Ok(userId);
                }
                else
                {
                    return BadRequest("User does not exist");
                }
            }
            catch
            {
                return BadRequest("User does not exist");
            }
        }
    }
}
