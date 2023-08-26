using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net;
using System.Text;

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
        public IFormFile? Upload { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //Uploads users image
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                //Get random file name for security
                var trustedFileName = Path.GetRandomFileName();
                trustedFileName = Path.ChangeExtension(trustedFileName, ".png");

                var file = Path.Combine(_env.ContentRootPath, @"wwwroot\UserPhotos", trustedFileName);

                if (Upload != null)
                {
                    //Upload file to server
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }

                    //Save File Path to database
                    var userPhoto = _context.UserImages.SingleOrDefault(x => x.Id == user.Photo.Id);
                    userPhoto.path = @"\UserPhotos\" + trustedFileName;
                    _context.SaveChanges();

                    StatusMessage = "Your profile has been updated";
                    return RedirectToPage();
                }
                else
                {
                    StatusMessage = "Error: Uploaded File Not Found!";
                    return RedirectToPage();
                }
            }
            else
            {
                StatusMessage = "Error: No User Found!";
                return RedirectToPage();
            }
        }
    }
}
