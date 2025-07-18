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
        public ActionResult CriarCarteira([FromBody] CarteiraModel novaCarteira)
        {
            if (novaCarteira == null || string.IsNullOrEmpty(novaCarteira.ClienteId))
                return BadRequest(new { mensagem = "Dados da carteira inválidos." });

            var idCarteira = _carteiraService.CriarCarteira(novaCarteira);

            return Ok(new
            {
                mensagem = "Carteira criada com sucesso.",
                idCarteira
            });
        }
        // Novo método: GET /carteiras/{clienteId}
        [HttpGet("{clienteId}")]
        public ActionResult ConsultarCarteira(string clienteId)
        {
            var carteira = _carteiraService.ObterCarteiraPorClienteId(clienteId);

            if (carteira == null)
            {
                return NotFound(new { mensagem = "Cliente não encontrado ou sem carteira." });
            }

            var valorTotal = _carteiraService.CalcularValorTotalCarteira(carteira);

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