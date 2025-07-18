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

        public List<AtivoModel> ObterAtivos()
        {
            return _ativoRepository.ListarAtivos();
        }
    }
}