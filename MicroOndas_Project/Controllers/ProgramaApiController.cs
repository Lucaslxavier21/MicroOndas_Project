using MicroOndas_Project.Models;
using MicroOndas_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroOndas_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaApiController : ControllerBase
    {
        private readonly ProgramaService _programaService;
        private readonly MicroondasService _aquecimentoService;

        public ProgramaApiController(ProgramaService service, MicroondasService aquecimentoService)
        {
            _programaService = service;
            _aquecimentoService = aquecimentoService;
        }
        [HttpGet("listar")]
        public async Task<IActionResult> ListarProgramas()
        {
            try
            {
                var programas = await _programaService.ObterTodosProgramasAsync();
                return Ok(programas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar programas", erro = ex.Message });
            }
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarPrograma([FromBody] ProgramaModel programa)
        {
            try
            {
                var id = await _programaService.AdicionarPrograma(programa);
                return CreatedAtAction(nameof(ListarProgramas), new { id }, programa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao cadastrar programa", erro = ex.Message });
            }
        }

        [HttpPost("iniciarAquecimento")]
        public IActionResult IniciarAquecimento([FromBody] int tempo, int potencia)
        {
            try
            {
                if (!_aquecimentoService.ValidarTempo(tempo))
                {
                    return StatusCode(400, new { sucesso = false, mensagem = "O tempo deve estar entre 1 e 120 segundos." });
                }

                if (!_aquecimentoService.ValidarPotencia(potencia))
                {
                    potencia = 10;
                }

                string tempoConvertido = _aquecimentoService.ConverterTempo(tempo);
                string stringAquecimento = _aquecimentoService.GerarStringAquecimento(tempo, potencia);

                return StatusCode(200, new
                {
                    sucesso = true,
                    tempoFormatado = tempoConvertido,
                    aquecimento = stringAquecimento
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao iniciar aquecimento", erro = ex.Message });
            }
        }
    }
}
