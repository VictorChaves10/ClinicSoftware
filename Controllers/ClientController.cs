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
            var clients = _clientRepository.Clients;

            return View(clients);
        }


        // GET: Client/Details/5
        public async Task<IActionResult> Details(int id)
        {       

            var client = await _clientRepository.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Client/Create
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

                // Processamento da imagem do perfil
                if (profileImage != null && profileImage.Length > 0)
                {
                    if (!_managerImage.IsImageFile(profileImage))
                    {
                        ModelState.AddModelError("ProfileImageUrl", "O arquivo enviado não é uma imagem válida.");
                        return View(client);
                    }

                    client.ProfileImageUrl = await _managerImage.SaveImageRepository(profileImage);
                }
                else
                {
                    client.ProfileImageUrl = "\\ProfileImages\\default-avatar.jpg";
                }

                await _clientRepository.AddClient(client);

                return RedirectToAction("Index", "Home");
            }

            return View(client);
        }


        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int id)
        
        {
            var client = await _clientRepository.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client, IFormFile? profileImage)
        {

            if(ModelState.IsValid)
            {
                var existingClient = await _clientRepository.GetClientById(id);

                // Processamento da imagem do perfil
                if (profileImage != null && profileImage.Length > 0)
                {
                    if (!_managerImage.IsImageFile(profileImage))
                    {
                        ModelState.AddModelError("ProfileImageUrl", "O arquivo enviado não é uma imagem válida.");
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
                    // Reatribuir a imagem existente para garantir que não haja perda de dados
                    client.ProfileImageUrl = existingClient.ProfileImageUrl;
                }

                await _clientRepository.EditClient(client);

                return RedirectToAction("Details", "Client", new { id = client.ClientId });

            }

            return View(client);
        }


        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
          
            var client = await _clientRepository.GetClientById(Id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int clientId)
        {
            var client = await _clientRepository.GetClientById(clientId);

            if (client == null)
            {
                return NotFound();
            }
            await _clientRepository.DeleteClient(clientId);

            return RedirectToAction(nameof(Index));
        }

    }
}
