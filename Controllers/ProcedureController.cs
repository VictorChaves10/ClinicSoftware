using ClinicSoftware.Models;
using ClinicSoftware.Repositories;
using ClinicSoftware.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSoftware.Controllers
{
    public class ProcedureController : Controller
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureController(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        public IActionResult Index()
        {
            var procedure = _procedureRepository.Procedures.ToList();
            return View(procedure);
        }

        // GET: Procedure/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var employee = await _procedureRepository.GetProcedureById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }



        // GET: Procedure/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Procedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Procedure procedure)
        {

            if (ModelState.IsValid)
            {
                await _procedureRepository.AddProcedure(procedure);

                TempData["SuccessMessage"] = "Procedimento adicionado com sucesso!";
                return RedirectToAction("Index", "Home");
            }

            return View(procedure);
        }

        // GET: Procedure/Edit/5
        public async Task<IActionResult> Edit(int id)

        {
            var procedure = await _procedureRepository.GetProcedureById(id);

            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }


        // POST: Procedure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Procedure procedure)
        {

            if (ModelState.IsValid)
            {
                await _procedureRepository.EditProcedure(procedure);

                return RedirectToAction("Details", "Procedure", new { id = procedure.ProcedureId });
            }

            return View(procedure);
        }

        // GET: Procedure/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {

            var procedure = await _procedureRepository.GetProcedureById(Id);

            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }


        // POST: Procedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int procedureId)
        {
            var procedure = await _procedureRepository.GetProcedureById(procedureId);

            if (procedure == null)
            {
                return NotFound();
            }
            await _procedureRepository.DeleteProcedure(procedureId);

            return RedirectToAction(nameof(Index));
        }
    }
}
