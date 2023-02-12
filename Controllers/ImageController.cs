using Microsoft.AspNetCore.Mvc;
using NotBlocket2.Models;
using Umbraco.Core.Persistence.Repositories;
using System.Web;
using Microsoft.AspNet;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NotBlocket2.Controllers {

    public class ImageController : Controller {

        public IActionResult Create() {
            return View();
        }

        private bool IsValidImageFormat(string fileName) {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }

        [HttpGet]
        public async Task<IActionResult> CreateAd() {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, Ad ad) {
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
            
            // Generate a unique file name
            var fileName = $"{DateTime.Now.ToString("yyyyMMddhhmmss")}_{new Random().Next(1000, 9999)}_{file.FileName}";

            // Save the file to the file system
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                await fileStream.WriteAsync(fileBytes);
            }

            //Ad ad = new Ad();
            //add path to ad
            ad.ImagePath = Path.Combine("/images", fileName);

			// Save the information to the database
			string errormsg = "";
            AdMethods am = new AdMethods();
            am.InsertAd(ad, out errormsg);
            ViewBag.error = errormsg;

			return RedirectToAction("Filtering","NotBlocket");
		}

        [HttpPost]
        public async Task<IActionResult> UpdateImage(IFormFile file, Ad ad) {
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

            // Generate a unique file name
            var fileName = $"{DateTime.Now.ToString("yyyyMMddhhmmss")}_{new Random().Next(1000, 9999)}_{file.FileName}";

            // Save the file to the file system
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                await fileStream.WriteAsync(fileBytes);
            }

            //Ad ad = new Ad();
            //add path to ad
            ad.ImagePath = Path.Combine("/images", fileName);

            // Save the information to the database
            string errormsg = "";
            AdMethods am = new AdMethods();
            am.InsertAd(ad, out errormsg);
            ViewBag.error = errormsg;

            return RedirectToAction("Filtering", "NotBlocket");

        }

        [HttpPost]
        public async Task<IActionResult> UpdtImagefunc(IFormFile file, Ad ad) {
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

            // Generate a unique file name
            var fileName = $"{DateTime.Now.ToString("yyyyMMddhhmmss")}_{new Random().Next(1000, 9999)}_{file.FileName}";

            // Save the file to the file system
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                await fileStream.WriteAsync(fileBytes);
            }

            //Ad ad = new Ad();
            //add path to ad
            ad.ImagePath = Path.Combine("/images", fileName);

            // Save the information to the database
            string errormsg = "";
            AdMethods am = new AdMethods();
            am.UpdateAd(ad, out errormsg);
            ViewBag.error = errormsg;

            return RedirectToAction("Filtering", "NotBlocket");

        }




    }


    }



    

