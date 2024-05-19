namespace ClinicSoftware.Services
{
    public interface IManagerImage
    {     
        bool IsImageFile(IFormFile file);
        Task<string> SaveImageRepository(IFormFile file);
        void DeleteImage(string imageUrl);
    }
}
