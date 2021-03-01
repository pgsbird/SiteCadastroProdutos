using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace SiteMercadoProdutos.Controllers
{
    [Route("api/UploadFile")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost,DisableRequestSizeLimit]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Temp");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave,fileName);
                    var dbPath = Path.Combine(folderName,fileName);

                    using (var stream = new FileStream(fullPath,FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new {dbPath});
                }
                else{
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                 return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}