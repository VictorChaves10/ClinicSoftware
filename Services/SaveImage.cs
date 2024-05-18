using Microsoft.AspNetCore.Hosting;

namespace ClinicSoftware.Services
{
    public class SaveImage : ISaveImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SaveImage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public bool IsImageFile(IFormFile file)
        {
            return file.ContentType.StartsWith("image/");
        }

        public string SaveImageRepository(IFormFile image)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return Path.Combine("\\", "ProfileImages", uniqueFileName);
        }

    }
}
