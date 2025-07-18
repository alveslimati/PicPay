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
        public ActionResult<List<AtivoModel>> ListarAtivos()
        {
            var ativos = _ativoService.ObterAtivos();
            
            return Ok(ativos);
        }
    }
}