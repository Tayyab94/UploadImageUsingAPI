using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UploadImageUsingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public ImageUploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }




        public class FileUpload
        {
            public IFormFile files { get; set; }
        }


        [HttpGet]
        public async Task<IActionResult> Getdata()
        {

            return Ok(new { tayyab = "tayyab", age = "12" });
        }

        [HttpPost]
        public async Task<String> Post([FromForm]FileUpload fileUpload)
        {
            try
            {
                if(fileUpload.files.Length>0)
                {
                    if(!Directory.Exists(environment.WebRootPath+"\\Upload\\"))
                    {
                        Directory.CreateDirectory(environment.WebRootPath + "\\Upload\\");
                    }

                    // First Check if the file exist of not.. 
                    if (System.IO.File.Exists(environment.WebRootPath + "\\Upload\\" + fileUpload.files.FileName))
                    {
                        System.IO.File.Delete(environment.WebRootPath + "\\Upload\\" + fileUpload.files.FileName);
                    }

                    // add files
                    using (FileStream fileStream = System.IO.File.Create(environment.WebRootPath + "\\Upload\\" + fileUpload.files.FileName))
                    {
                      
                        fileUpload.files.CopyTo(fileStream);
                        fileStream.Flush();

                        return "\\Upload\\" + fileUpload.files.FileName;
                    }
                }
                else
                {
                    return "File Not Found";
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        
    }
}
