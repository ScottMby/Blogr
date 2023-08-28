using Blogr.Data;
using Blogr.Server.Controllers;
using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Blogr.Server.Hubs
{
    public class AnalyticsHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public AnalyticsHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task BlogViews(int views, int bId)
        {
            await Clients.All.SendAsync("GetBlogViews", views, bId);
        }

        public async Task BlogViewChange(int views, int bId)
        {
            var blog = _context.Blogs
                .Where(u => u.ID == bId)
                .Include("Analytics")
                .FirstOrDefault();

            if(blog != null)
            {
                blog.Analytics.Views += views;
                _context.SaveChanges();
                await BlogViews(blog.Analytics.Views, bId);
            }
        }

    }
}
