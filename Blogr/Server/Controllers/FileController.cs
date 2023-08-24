using Blogr.Server.Data;
using Blogr.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blogr.Server.Controllers
{
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<ActionResult<List<UploadResult>>> UploadBlogContent(List<IFormFile> files)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                string untrustedFileName = file.FileName;
                uploadResult.FileName = file.FileName;
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);


                trustedFileNameForFileStorage = Path.GetRandomFileName(); 
                trustedFileNameForFileStorage = Path.ChangeExtension(trustedFileNameForFileStorage, ".pdf");
                var path = Path.Combine(_env.ContentRootPath, @"wwwroot\BlogContent", trustedFileNameForFileStorage);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                uploadResult.StoredFileName = @"\BlogContent\" + trustedFileNameForFileStorage;
                uploadResult.Uploaded = true;
                uploadResults.Add(uploadResult);
            }

            //Add blog content to new blog

            return Ok(uploadResults);
        }
    }
}
