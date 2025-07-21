using ApiCarteiraInvestimentos.Models;
using ApiCarteiraInvestimentos.Mocks;

namespace ApiCarteiraInvestimentos.Repositories
{
    public class AtivoRepository : IAtivoRepository
    {
        public async Task<List<AtivoModel>> ListarAtivosAsync()
        {
            return await Task.FromResult(AtivosMock.GetAtivos());
        }
    }
}