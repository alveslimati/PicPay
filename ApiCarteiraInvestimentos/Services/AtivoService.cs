using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Repositories;

namespace ApiCarteiraInvestimentos.Services
{
    public class AtivoService
    {
        private readonly AtivoRepository _ativoRepository;

        public AtivoService()                                                                                       
        {
            _ativoRepository = new AtivoRepository();
        }

        public async Task<List<AtivoModel>> ObterAtivosAsync()
        {
            return await _ativoRepository.ListarAtivosAsync();
        }
    }
}