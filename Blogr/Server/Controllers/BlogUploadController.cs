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
using Microsoft.AspNetCore.Authorization;

namespace Blogr.Server.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<string>> UploadBlog(BlogUpload? bu)
        {
            if (bu != null)
            {
                try
                {
                    //If BlogId is null then we add a new blog
                    if (bu.BlogId == null)
                    {
                        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
                        if (user != null)
                        {
                            var bc = new BlogContent
                            {
                                path = bu.ContentPath
                            };

                            var bl = new Blog
                            {
                                Title = bu.Title,
                                Category = bu.Category,
                                User = user,
                                CreationDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                Content = bc,
                                Analytics = new BlogAnalytics()
                            };

                            _context.Add(bl);
                            await _context.SaveChangesAsync();
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
                        var bc = new BlogContent
                        {
                            path = bu.ContentPath
                        };


                        var currentBlog = _context.Blogs
                            .FirstOrDefault(u => u.ID == bu.BlogId);

                        if (currentBlog != null)
                        {
                            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
                            if (user != null)
                            {
                                currentBlog.Title = bu.Title;
                                currentBlog.Category = bu.Category;
                                currentBlog.CreationDate = currentBlog.CreationDate;
                                currentBlog.UpdatedDate = DateTime.Now;
                                currentBlog.Content = bc;
                                await _context.SaveChangesAsync();
                                return Ok("Blog Uploaded");
                            }
                            else
                            {
                                return BadRequest("There is no Current User");
                            }
                        }

                        else
                        {
                            return BadRequest("Blog Not Found");
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
