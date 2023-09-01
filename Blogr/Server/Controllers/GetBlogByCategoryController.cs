﻿using Blogr.Client.Pages;
using Blogr.Data;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogr.Server.Controllers
{
    [Route("api/GetBlogByCategory")]
    [ApiController]
    public class GetBlogByCategoryController : GetBlogBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetBlogByCategoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("{category}")]
        [HttpGet]
        public async Task<ActionResult<List<BlogDisplay>>> Get([FromRoute] string category)
        {
            try
            {
                List<BlogDisplay> blogList = new List<BlogDisplay>();

                List<Blog> blogs = _context.Blogs
                    .Where(b => b.Category == category)
                    .Where(b => b.UpdatedDate > DateTime.Now.AddDays(-7))
                    .Include("User")
                    .Include("Analytics")
                    .Include("Content")
                    .OrderBy(b => b.Analytics.UniqueVisitors)
                    .ToList();
                return GetBlogDisplays(blogs, false).Result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}
