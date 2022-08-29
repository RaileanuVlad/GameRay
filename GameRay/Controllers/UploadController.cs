using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GameRay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("Secret1");

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
         

                if(file.Length > 0)
                {
                    var _task = Task.Run(() => this.UploadToAzureAsync(file));
                    _task.Wait();
                    string fileUrl = _task.Result;
                    return Ok(new {fileUrl });
                    
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd") + "/" + strName[0] + "(" + DateTime.Now.AddHours(3).ToString("HH:mm:ss.fff") + ")" + "." + strName[strName.Length - 1];
            return strFileName;
        }
        private async Task<string> UploadToAzureAsync(IFormFile file)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("resources");
            if(await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(GenerateFileName(file.FileName));
            cloudBlockBlob.Properties.ContentType = file.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return cloudBlockBlob.Uri.AbsoluteUri;
        }
    }
}
