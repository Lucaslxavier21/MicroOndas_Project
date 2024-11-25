using MicroOndas_Project.Models;
using MicroOndas_Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroOndas_Project.Controllers
{
    public class MicroondasController : Controller
    {
        private readonly MicroondasService _aquecimentoService;
        private readonly ProgramaService _service;
        public MicroondasController(MicroondasService aquecimentoService, ProgramaService service)
        {
            _aquecimentoService = aquecimentoService;
            _service = service;
        }
        public async Task<IActionResult> TelaInicial()
        {
            var listaProgramas = await _service.ObterTodosProgramasAsync();
            return View(listaProgramas);
        }
        public IActionResult IniciarAquecimento(int tempo, int potencia)
        {
            try
            {
                if (!_aquecimentoService.ValidarTempo(tempo))
                {
                    return Json(new { sucesso = false, mensagem = "O tempo deve estar entre 1 e 120 segundos." });
                }

                if (!_aquecimentoService.ValidarPotencia(potencia))
                {
                    potencia = 10; 
                }

                string tempoConvertido = _aquecimentoService.ConverterTempo(tempo);
                string stringAquecimento = _aquecimentoService.GerarStringAquecimento(tempo, potencia);

                return Json(new
                {
                    sucesso = true,
                    tempoFormatado = tempoConvertido,
                    aquecimento = stringAquecimento
                });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
        public async Task<IActionResult> ObterProgramaPreDefinido(string nome)
        {
            var programa = await _service.ObterProgramaPorNome(nome);
            if (programa == null)
            {
                return Json(new { sucesso = false, mensagem = "Programa não encontrado." });
            }

            return Json(new
            {
                sucesso = true,
                tempo = programa.Tempo,
                potencia = programa.Potencia,
                mensagem = $"Programa {programa.Nome} carregado com sucesso."
            });
        }
    }
}
