using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Blogr.Shared;
using System.Security.Claims;
using Blogr.Data;
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Blogr.Server.Controllers
{
    [Route("api/BlogUpload")]
    [ApiController]
    public class BlogUploadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public BlogUploadController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; //For database operations
            _userManager = userManager; //For getting the current user
        }

        /// <summary>
        /// Adds or Updates the database to include the new blog post that has been uploaded.
        /// </summary>
        /// <param name="bu"></param>
        /// <returns>Success String</returns>
        [HttpPost]
        public async Task<ActionResult<string>> UploadBlog(BlogUpload bu)
        {
            if(bu != null)
            {
                try
                {
                    //If BlogId is null then we add a new blog
                    if (bu.BlogId == null)
                    {
                        BlogContent bc = new BlogContent();
                        bc.path = bu.ContentPath;

                        Blog bl = new Blog();

                        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        if(user != null)
                        {
                            bl.Title = bu.Title;
                            bl.Category = bu.Category;
                            bl.User = user;
                            bl.CreationDate = DateTime.Now;
                            bl.UpdatedDate = DateTime.Now;
                            bl.Content = bc;
                            bl.Analytics = new BlogAnalytics();
                            _context.Add(bl);
                            _context.SaveChanges();
                            return Ok("Blog Uploaded");
                        }
                        else
                        {
                            return BadRequest("There is no Current User");
                        }
                        
                    }
                    //if a BlogId is present then update the blog associated with the Id
                    else
                    {
                        BlogContent bc = new BlogContent();
                        bc.path = bu.ContentPath;

                        var currentBlog = _context.Blogs
                        .Where(u => u.ID == bu.BlogId)
                        .FirstOrDefault();

                        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        if (user != null)
                        {
                            currentBlog.Title = bu.Title;
                            currentBlog.CreationDate = currentBlog.CreationDate;
                            currentBlog.UpdatedDate = DateTime.Now;
                            currentBlog.Content = bc;
                            _context.SaveChanges();
                            return Ok("Blog Uploaded");
                        }
                        else
                        {
                            return BadRequest("There is no Current User");
                        }
                    }
                }
                catch
                {
                    return BadRequest("Blog Upload Failed");
                }
            }
            else
            {
                return BadRequest("Blog Upload Parameter is Null");
            }
           
        }
    }
}
