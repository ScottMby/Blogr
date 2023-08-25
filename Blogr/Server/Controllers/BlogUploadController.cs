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
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<string>> UploadBlog(BlogUpload bu)
        {
            //Add New blog to server
            try
            {
                if (bu.BlogId == null)
                {
                    BlogContent bc = new BlogContent();
                    bc.path = bu.ContentPath;
                    Blog bl = new Blog();
                    bl.b_Title = bu.Title;
                    bl.b_User = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    bl.b_CreationDate = DateTime.Now;
                    bl.b_UpdatedDate = DateTime.Now;
                    bl.b_Content = bc;
                    _context.Add(bl);
                    _context.SaveChanges();
                    return Ok("Blog Uploaded");
                }
                else
                {
                    BlogContent bc = new BlogContent();
                    var currentBlog = _context.Blogs
                    .Where(u => u.b_ID == bu.BlogId)
                    .FirstOrDefault();
                    bc.path = bu.ContentPath;
                    currentBlog.b_Title = bu.Title;
                    currentBlog.b_User = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    currentBlog.b_CreationDate = currentBlog.b_CreationDate;
                    currentBlog.b_UpdatedDate = DateTime.Now;
                    currentBlog.b_Content = bc;
                    await _context.Entry(currentBlog).ReloadAsync();
                    _context.SaveChanges();
                    return Ok("Blog Uploaded");
                }
            }
            catch
            {
                return BadRequest("Blog Upload Failed");
            }
        }
    }
}
