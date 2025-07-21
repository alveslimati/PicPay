using ApiCarteiraInvestimentos.Models;

namespace ApiCarteiraInvestimentos.Repositories
{
    public interface ICarteiraRepository
    {
        Task<CarteiraModel?> ObterCarteiraPorClienteIdAsync(string clienteId);
        Task<string> CriarCarteiraAsync(CarteiraModel carteira);
    }
}