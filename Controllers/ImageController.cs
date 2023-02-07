using Microsoft.AspNetCore.Mvc;
using NotBlocket2.Models;
using Umbraco.Core.Persistence.Repositories;
using System.Web;
using Microsoft.AspNet;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NotBlocket2.Controllers {

    [Route("api/[controller]")]
    public class ImageController : Controller {

        public IActionResult Create() {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file) {
            // Check if the file is null
            if (file == null || file.Length == 0) {
                return BadRequest("File is empty or missing");
            }

            // Get the file stream and read its contents
            var stream = file.OpenReadStream();
            var fileBytes = new byte[file.Length];
            await stream.ReadAsync(fileBytes, 0, (int)file.Length);

            // Validate the file format
            if (!IsValidImageFormat(file.FileName)) {
                return BadRequest("Invalid file format");
            }

            // Save the file to the file system
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                await fileStream.WriteAsync(fileBytes);
            }

            // Save the file information to the database
            // ...

            return Ok();
        }

        private bool IsValidImageFormat(string fileName) {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }
    }



    }

