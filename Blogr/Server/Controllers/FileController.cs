using Blogr.Server.Data;
using Blogr.Server.Models;
using Blogr.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blogr.Server.Controllers
{
    [Authorize]
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FileController(IWebHostEnvironment env)
        {
            _env = env; //For getting server directory
        }
        /// <summary>
        /// Uploads the file/s into the server.
        /// </summary>
        /// <param name="files"></param>
        /// <returns>A list of type UploadResult</returns>
        [HttpPost]
        public async Task<ActionResult<List<UploadResult>>> UploadBlogContent(List<IFormFile> files)
        {
            try
            {
                List<UploadResult> uploadResults = new List<UploadResult>();

                foreach (var file in files)
                {
                    var uploadResult = new UploadResult();

                    //Change filename to random name for security

                    string trustedFileNameForFileStorage;
                    string untrustedFileName = file.FileName;

                    uploadResult.FileName = file.FileName;

                    var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);
                    trustedFileNameForFileStorage = Path.GetRandomFileName();
                    trustedFileNameForFileStorage = Path.ChangeExtension(trustedFileNameForFileStorage, ".pdf");

                    //Get path for storing file
                    var path = Path.Combine(_env.ContentRootPath, @"wwwroot\BlogContent", trustedFileNameForFileStorage);


                    //Store File
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    uploadResult.StoredFileName = @"\BlogContent\" + trustedFileNameForFileStorage;
                    uploadResult.Uploaded = true;
                    uploadResults.Add(uploadResult);
                }
                //Return Uploaded File Name
                return Ok(uploadResults);
            }
            catch
            {
                return BadRequest("File Upload Failed");
            }
        }
    }
}
