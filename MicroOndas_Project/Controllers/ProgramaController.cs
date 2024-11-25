using MicroOndas_Project.Models;
using MicroOndas_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroOndas_Project.Controllers
{
    public class ProgramaController : Controller
    {
        private readonly ProgramaService _programaService;
        public ProgramaController(ProgramaService service)
        {
            _programaService = service;
        }
        public async Task<IActionResult> Index()
        {
            var listaProgramas = await _programaService.ObterTodosProgramasAsync();
            return View(listaProgramas);
        }
        public async Task<IActionResult> AdicionarPrograma(ProgramaModel model)
        {
            try
            {
                await _programaService.AdicionarPrograma(model);
                TempData["MensagemSucesso"] = "Programa customizado cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
