using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace Blogr.Server.Controllers
{
    [Authorize]
    [Route("api/userImage")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserImageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; //For database operations
            _userManager = userManager; //For getting the current user
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetUserImage()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                var user = await _userManager.FindByIdAsync(claim);
                if (user != null)
                {
                    return Ok(user.u_Photo.ToJson());
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
    }
}
