using ClinicSoftware.Models;
using ClinicSoftware.Repositories.Interfaces;
using ClinicSoftware.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftware.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IManagerImage _managerImage;

        public ClientController(IClientRepository clientRepository, IManagerImage managerImage)
        {
            _clientRepository = clientRepository;
            _managerImage = managerImage;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client, IFormFile? profileImage)
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
                    if (!_managerImage.IsImageFile(profileImage))
                    {
                        ModelState.AddModelError("ProfileImage", "O arquivo enviado não é uma imagem válida.");
                        return View(client);
                    }

                    client.ProfileImageUrl = await _managerImage.SaveImageRepository(profileImage);
                }

                await _clientRepository.AddClient(client);

                return RedirectToAction("Index", "Home");
            }

            return View(client);
        }

        public async Task<IActionResult> Edit(int id)
        
        {
            var client = await _clientRepository.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client, IFormFile? profileImage)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingClient = await _clientRepository.GetClientById(id);

                if (profileImage != null && profileImage.Length > 0)
                {
                    if (!_managerImage.IsImageFile(profileImage))
                    {
                        ModelState.AddModelError("ProfileImage", "O arquivo enviado não é uma imagem válida.");
                        return View(client);
                    }

                    if (!string.IsNullOrEmpty(existingClient.ProfileImageUrl))
                    {
                        _managerImage.DeleteImage(existingClient.ProfileImageUrl);
                    }

                    client.ProfileImageUrl = await _managerImage.SaveImageRepository(profileImage);
                }
                else
                {
                    client.ProfileImageUrl = existingClient.ProfileImageUrl;
                }

                await _clientRepository.EditClient(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

    }
}
