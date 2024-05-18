using Microsoft.AspNetCore.Hosting;

namespace ClinicSoftware.Services
{
    public class ManegerImage : IManagerImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManegerImage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public bool IsImageFile(IFormFile file)
        {
            return file.ContentType.StartsWith("image/");
        }

        public async Task<string> SaveImageRepository(IFormFile image)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return Path.Combine("\\", "ProfileImages", uniqueFileName);
        }

        public void DeleteImage(string imageUrl)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
