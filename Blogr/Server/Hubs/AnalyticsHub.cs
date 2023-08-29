using Blogr.Data;
using Blogr.Server.Controllers;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Blogr.Server.Hubs
{
    public class AnalyticsHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AnalyticsHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task BlogViews(int views, int bId)
        {
            await Clients.All.SendAsync("GetBlogViews", views, bId);
        }

        public async Task BlogViewChange(int views, int bId, string userId)
        {
            var blog = _context.Blogs
                .Where(u => u.ID == bId)
                .Include(b => b.Analytics)
                .ThenInclude(c => c.Viewers)
                .FirstOrDefault();

            if (blog != null)
            {
                blog.Analytics.Views += views;

                if (userId != null)
                {
                    var viewer = blog.Analytics.Viewers
                        .FirstOrDefault(u => u.UserId.ToString() == userId);
                    if (viewer == null)
                    {
                        BlogViewers bv = new BlogViewers();
                        bv.UserId = userId;
                        blog.Analytics.Viewers.Add(bv);
                        blog.Analytics.UniqueVisitors += 1;
                    }
                }
                    
                _context.SaveChanges();
                await BlogViews(blog.Analytics.Views, bId);
            }
        }

    }
}
