using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Blogr.Server.Areas.Identity.Pages.Account.Manage
{
    public class ChangePhotoModel : PageModel
    {
        private IWebHostEnvironment _env;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        public ChangePhotoModel(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _env = env;
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var TrustedFileName = Path.GetRandomFileName();
            TrustedFileName = Path.ChangeExtension(TrustedFileName, ".png");
            var file = Path.Combine(_env.ContentRootPath, @"wwwroot\UserPhotos", TrustedFileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            var userPhoto = _context.UserImages.SingleOrDefault(x => x.Id == user.u_Photo.Id);
            userPhoto.path = @"\UserPhotos\" + TrustedFileName;
            _context.SaveChanges();
        }
    }
}
