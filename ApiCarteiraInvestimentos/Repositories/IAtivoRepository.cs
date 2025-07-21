using ApiCarteiraInvestimentos.Models;

namespace ApiCarteiraInvestimentos.Repositories
{
    public interface IAtivoRepository
    {
        Task<List<AtivoModel>> ListarAtivosAsync();
    }
}