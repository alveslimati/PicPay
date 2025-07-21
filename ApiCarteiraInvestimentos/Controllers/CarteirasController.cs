using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCarteiraInvestimentos.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CarteirasController : ControllerBase
    {
        private readonly CarteiraService _carteiraService;

        public CarteirasController(CarteiraService carteiraService)
        {
            _carteiraService = carteiraService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCarteira([FromBody] CarteiraModel novaCarteira)
        {
            if (novaCarteira == null)
                return BadRequest(new { mensagem = "A carteira não pode ser nula." });

            if (string.IsNullOrWhiteSpace(novaCarteira.ClienteId))
                return BadRequest(new { mensagem = "O ID do cliente é obrigatório." });

            try
            {
                var idCarteira = await _carteiraService.CriarCarteiraAsync(novaCarteira);

                return Ok(new
                {
                    mensagem = "Carteira criada com sucesso.",
                    idCarteira
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro ao criar a carteira." });
            }
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ConsultarCarteira(string clienteId)
        {
            var carteira = await _carteiraService.ObterCarteiraPorClienteIdAsync(clienteId);

            if (carteira == null)
            {
                return NotFound(new { mensagem = "Cliente não encontrado ou sem carteira." });
            }

            var valorTotal = await _carteiraService.CalcularValorTotalCarteiraAsync(carteira);

            var response = new CarteiraModel
            {
                ClienteId = carteira.ClienteId,
                Ativos = carteira.Ativos,
                ValorTotal = valorTotal
            };

            return Ok(response);
        }
    }
}