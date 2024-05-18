namespace ClinicSoftware.Services
{
    public interface ISaveImage
    {
        public bool IsImageFile(IFormFile file);
        public string SaveImageRepository(IFormFile image);
    }
}
