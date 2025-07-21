using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCarteiraInvestimentos.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AtivosController : ControllerBase
    {
        private readonly AtivoService _ativoService;
        public AtivosController() => _ativoService = new AtivoService();

        [HttpGet]
        public async Task<IActionResult> ListarAtivos()
        {
            var ativos = await _ativoService.ObterAtivosAsync(); 

            return Ok(ativos);
        }
    }
}