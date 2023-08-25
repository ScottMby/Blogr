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
            List<BlogDisplay> blogList = new List<BlogDisplay>();
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if(user != null)
            {
                var userBlogs = _context.Blogs
                .Where(u => u.b_User == user)
                .Include("b_Content"); //To ensure that the blog content is also retrieved
                if(userBlogs != null)
                {
                    foreach (var blog in userBlogs)
                    {
                        BlogDisplay blogDisplay = new BlogDisplay();
                        blogDisplay.Id = blog.b_ID;
                        blogDisplay.Title = blog.b_Title;
                        if (user.u_Photo.path != null)
                        {
                            blogDisplay.CreatorImgPath = user.u_Photo.path;
                        }
                        else
                        {
                            blogDisplay.CreatorImgPath = @"~\UserPhotos\default.png";
                        }
                        blogDisplay.CreatorFirstName = user.u_FirstName;
                        blogDisplay.CreatorLastName = user.u_LastName;
                        blogDisplay.CreationDate = blog.b_CreationDate;
                        blogDisplay.UpdatedDate = blog.b_UpdatedDate;
                        blogDisplay.ContentId = blog.b_Content.Id;
                        blogDisplay.ContentPath = blog.b_Content.path;
                        blogList.Add(blogDisplay);
                    }
                    return Ok(blogList);
                }
                else
                {
                    return BadRequest("No Blogs Found");
                }   
            }
            else
            {
                return BadRequest("Current User Not Found");
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
            List<BlogDisplay> blogList = new List<BlogDisplay>();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userBlogs = _context.Blogs
                .Where(u => u.b_User == user)
                .Include("b_Content"); //To ensure that the blog content is also retrieved
                if (userBlogs != null)
                {
                    foreach (var blog in userBlogs)
                    {
                        BlogDisplay blogDisplay = new BlogDisplay();
                        blogDisplay.Id = blog.b_ID;
                        blogDisplay.Title = blog.b_Title;
                        if (user.u_Photo.path != null)
                        {
                            blogDisplay.CreatorImgPath = user.u_Photo.path;
                        }
                        else
                        {
                            blogDisplay.CreatorImgPath = @"~\UserPhotos\default.png";
                        }
                        blogDisplay.CreatorFirstName = user.u_FirstName;
                        blogDisplay.CreatorLastName = user.u_LastName;
                        blogDisplay.CreationDate = blog.b_CreationDate;
                        blogDisplay.UpdatedDate = blog.b_UpdatedDate;
                        blogDisplay.ContentId = blog.b_Content.Id;
                        blogDisplay.ContentPath = blog.b_Content.path;
                        blogList.Add(blogDisplay);
                    }
                    return Ok(blogList);
                }
                else
                {
                    return BadRequest("No Blogs Found");
                }
            }
            else
            {
                return BadRequest("Current User Not Found");
            }
        }
    }
}