using ClinicSoftware.Models;
using ClinicSoftware.Repositories.Interfaces;
using ClinicSoftware.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftware.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISaveImage _saveImage;

        public ClientController(IClientRepository clientRepository, ISaveImage saveImage)
        {
            _clientRepository = clientRepository;
            _saveImage = saveImage;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client, IFormFile profileImage)
        {
            
            if (ModelState.IsValid)
            {
                var clientExist = _clientRepository.Clients
                                                .FirstOrDefault(c => c.ClientPhoneNumber == client.ClientPhoneNumber);

                if (clientExist != null)
                {
                    ModelState.AddModelError("ClientPhoneNumber", "O cliente com esse número de telefone já existe.");
                    return View(client);
                }

                if (profileImage != null && profileImage.Length > 0)
                {
                    if (!_saveImage.IsImageFile(profileImage))
                    {
                        ModelState.AddModelError("ProfileImage", "O arquivo enviado não é uma imagem válida.");
                        return View(client);
                    }

                    client.ProfileImageUrl = _saveImage.SaveImageRepository(profileImage);
                }

                _clientRepository.AddClient(client);

                TempData["SuccessMessage"] = "Cliente adicionado com sucesso!";

                return RedirectToAction("Index", "Home");
            }

            return View(client);
        }

    }
}
